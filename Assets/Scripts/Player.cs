using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Camera))]
public class Player : MonoBehaviour
{

    public float Speed;
    public float RotateSpeed;
    public float PushPower;

    public float Reach;
    public Transform Hand;

    private bool Frozen = false;
    private bool SkipFrame = false;

    private readonly float CastRadius = .25f;

    private CharacterController Controller;
    private Camera Cam;

    private GameObject HeldItem = null;

    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SkipFrame)
        {
            SkipFrame = false;
            return;
        }

        if (!Frozen)
        {
            // Rotate
            transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("LookX") * RotateSpeed, Space.World);
            Cam.transform.Rotate(transform.right, Time.deltaTime * Input.GetAxis("LookY") * RotateSpeed, Space.World);

            // Move forward / backward
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            Vector3 move = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal"));
            move.Normalize();
            Controller.SimpleMove(move * Speed);

            // Interact objects
            if (Input.GetButtonDown("Fire1"))
                interactObject();

            // Pickup objects
            if (Input.GetButtonDown("Fire2"))
                pickupObject();

            // Push objects
            if (Input.GetButtonDown("Fire3"))
                pushObject();
        }

        if (HeldItem)
        {
            HeldItem.transform.position = Hand.position;
        }
    }
    
    private void interactObject()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Cam.transform.position, CastRadius, Cam.transform.forward, out hit, Reach,
            (1 << LayerMask.NameToLayer("Door")) | (1 << LayerMask.NameToLayer("Interactable"))))
        {
            Interactable i = hit.collider.gameObject.GetComponent<Interactable>();

            if (i)
                i.OnInteract.Invoke();
        }
    }

    private void pushObject()
    {
        if (HeldItem)
        {
            Rigidbody r = HeldItem.GetComponent<Rigidbody>();
            dropObject();
            if (r)
                r.AddForce(Cam.transform.forward * PushPower * 3);
            return;
        }

        RaycastHit hit;
        if (Physics.SphereCast(Cam.transform.position, CastRadius, Cam.transform.forward, out hit, Reach, ~(1 << LayerMask.NameToLayer("Player"))))
        {
            Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();

            if (rb)
                rb.AddForceAtPosition(Cam.transform.forward * PushPower, hit.point);
        }
    }

    private void pickupObject()
    {
        if (HeldItem)
        {
            dropObject();
            return;
        }
            

        RaycastHit hit;
        if (Physics.SphereCast(Cam.transform.position, CastRadius, Cam.transform.forward, out hit, Reach, (1 << LayerMask.NameToLayer("Pickupable"))))
        {
            Pickupable p = hit.collider.gameObject.GetComponent<Pickupable>();
            
            if (p)
            {
                HeldItem = hit.collider.gameObject;
                Rigidbody r = HeldItem.GetComponent<Rigidbody>();
                if (r)
                    r.freezeRotation = true;

                p.OnPickedUp.Invoke();
            }
        }
    }

    private void dropObject()
    {
        Pickupable p = HeldItem.GetComponent<Pickupable>();
        Rigidbody r = HeldItem.GetComponent<Rigidbody>();
        if (r)
        {
            r.velocity = Vector3.zero;
            r.freezeRotation = false;
        }
        HeldItem = null;
        p.OnDropped.Invoke();
    }

    public void Freeze(bool f)
    {
        Frozen = f;
        SkipFrame = true;
    }
}