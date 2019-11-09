using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyResolver : MonoBehaviour
{
    public List<GameObject> Keys;

    public bool DeleteKey;

    public UnityEvent OnKeyResolve;

    private void OnCollisionEnter(Collision collision)
    {
        if (Keys.Contains(collision.gameObject))
        {
            OnKeyResolve.Invoke();
            if (DeleteKey)
                Destroy(collision.gameObject);
        }
    }
}
