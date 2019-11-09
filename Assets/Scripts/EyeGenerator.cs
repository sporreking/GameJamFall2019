using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeGenerator : MonoBehaviour
{
    public Eye eye;
    public uint NumberOfEyes = 20;
    public uint radius = 100;
    public float downScalerY = 2;
    // Start is called before the first frame update
    void Start()
    {
        float angleVertical;
        float angleHorizontal;
        Vector3 spawnPoint;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
        for (int i = 0; i < NumberOfEyes; i++)
        {
            angleVertical = Random.value * Mathf.PI;
            angleHorizontal = Random.value * 2 * Mathf.PI;
            float yPos = Mathf.Sin(angleVertical);
            spawnPoint = new Vector3(Mathf.Cos(angleVertical) * Mathf.Cos(angleHorizontal),
                yPos/(1 + Mathf.Pow(yPos,downScalerY)),
                Mathf.Cos(angleVertical) * Mathf.Sin(angleHorizontal))*radius;
            
            
            Instantiate(eye, spawnPoint, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}