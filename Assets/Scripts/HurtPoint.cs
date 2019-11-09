using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPoint : MonoBehaviour
{
    public float HurtRadius;

    private GameObject p;

    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = transform.position - p.transform.position;

        if (d.magnitude < HurtRadius)
        {
            p.GetComponent<Player>().SetChaosFactor(1.0f - d.magnitude / HurtRadius);
        }
    }
}
