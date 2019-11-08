using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool Locked;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (Locked)
            Lock() ;
    }


    public void Unlock()
    {
        Locked = false;
        rb.freezeRotation = false;
    }

    public void Lock()
    {
        rb.freezeRotation = true;
    }
}
