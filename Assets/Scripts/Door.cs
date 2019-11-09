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
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Debug.Log(GetComponent<Rigidbody>().GetInstanceID());
    }

    public void Lock()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log(GetComponent<Rigidbody>().GetInstanceID());
    }
}
