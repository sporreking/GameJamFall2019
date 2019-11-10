using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool Locked;

    private void Start()
    {
        if (Locked)
            Lock() ;
    }


    public void Unlock()
    {
        Locked = false;
        Rigidbody r = GetComponent<Rigidbody>();
        r.constraints = RigidbodyConstraints.None;
        r.transform.Rotate(0, 90, 0);
    }

    public void Lock()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(GetComponent<Rigidbody>().GetInstanceID());
    }
}
