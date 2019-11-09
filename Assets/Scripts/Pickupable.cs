using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickupable : MonoBehaviour
{
    public UnityEvent OnPickedUp;
    public UnityEvent OnDropped;

    private void Reset()
    {
        // Set layer when adding component to gameObject
        gameObject.layer = LayerMask.NameToLayer("Pickupable");
    }
}