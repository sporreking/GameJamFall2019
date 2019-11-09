using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/LookAtPlayer")]
public class LookAtPlayer : AIBehaviour
{
    public float speed = 5f;
    private Camera playerCamera;
    public override void DrawDebug(GameObject gameObject)
    {
        
    }

    public override void Execute(GameObject gameObject)
    {
        Transform targetTransform = playerCamera.transform;
        Vector3 direction = targetTransform.position - gameObject.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, speed * Time.deltaTime);
    }

    public override void StartBehaviour(GameObject gameObject)
    {
        playerCamera = GameObject.FindObjectOfType<Player>().gameObject.GetComponentInChildren<Camera>();
    }
}
