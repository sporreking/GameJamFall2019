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
        // Rotate
        transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("LookX") * RotateSpeed, Space.World);
        Cam.transform.Rotate(transform.right, Time.deltaTime * Input.GetAxis("LookY") * RotateSpeed, Space.World);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 move = (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal"));
        move.Normalize();
        Controller.SimpleMove(move * Speed);

        if (Input.GetButtonDown("Fire2"))
            pickupObject();

        if (Input.GetButtonDown("Fire3"))
            dropObject();

        if (HeldItem)
        {
            HeldItem.transform.position = Hand.position;
        }
     
    }
    
    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.AddForce(pushDir * PushPower);
    }*/

    private void pickupObject()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Cam.transform.position, CastRadius, Cam.transform.forward, out hit, Reach, (1 << LayerMask.NameToLayer("Pickupable"))))
        {
            Pickupable p = hit.collider.gameObject.GetComponent<Pickupable>();
            
            if (p && !HeldItem)
            {
                HeldItem = hit.collider.gameObject;
                p.OnPickedUp.Invoke();
            }
        }
    }

    private void dropObject()
    {
        if (!HeldItem)
            return;

        Pickupable p = HeldItem.GetComponent<Pickupable>();
        HeldItem = null;
        p.OnDropped.Invoke();
    }
}