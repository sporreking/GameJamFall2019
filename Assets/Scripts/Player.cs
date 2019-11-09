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

    private CharacterController Controller;
    private Camera Cam;

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
        

        // Push objects
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, Reach, ~(1 << LayerMask.NameToLayer("Player"))))
            {
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();

                Debug.Log(hit.collider.gameObject.name);

                if (rb)
                    rb.AddForceAtPosition(Cam.transform.forward * PushPower, hit.point);
            }
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
}
