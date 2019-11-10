using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPoint : MonoBehaviour
{
    public float HurtRadius;

    private GameObject p;

    public bool Invert;
    public float InnerRadius;
    private float CurrentChaosFactor = 0;

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
            CurrentChaosFactor = (Invert ? Mathf.Max(d.magnitude - InnerRadius, 0) / (HurtRadius - InnerRadius)
                : 1.0f - d.magnitude / HurtRadius);
            p.GetComponent<Player>().SetChaosFactor();
        }
    }
    public float GetChaosFactor()
    {
        return CurrentChaosFactor;
    }

    public void SetChaosFactor(float f)
    {
        CurrentChaosFactor = 0;
    }
}
