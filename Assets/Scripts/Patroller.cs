using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "AI/Patroller")]
public class Patroller : AIBehaviour
{

    public float SpottingDistance; // Within which distance can the patroller see the player?
    public float NotSpottingDistance; // If player runs far enough away, can't spot anymore.

    public UnityEvent PlayerSpotted;
    public UnityEvent PlayerLost;

    private NavMeshAgent Agent;
    private Player Player;
    private Camera Camera;
    private int DestPos = 1;
    private Transform[] Points;
    private bool IsPlayerSpotted = false;

    private readonly float DestinationSpacing = 1f;

    public override void DrawDebug(GameObject gameObject)
    {
    }

    public override void Execute(GameObject gameObject)
    {
        if (!Agent.pathPending && Agent.remainingDistance < DestinationSpacing)
            GoToNextPoint();

        if (!IsPlayerSpotted && (Player.transform.position - Agent.transform.position).magnitude <= SpottingDistance)
            PlayerSpotted.Invoke();

        if (IsPlayerSpotted && (Player.transform.position - Agent.transform.position).magnitude > NotSpottingDistance)
            PlayerLost.Invoke();
    }

    public override void StartBehaviour(GameObject gameObject)
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Agent.autoBraking = false;

        Player = GameObject.FindObjectOfType<Player>();
        Camera = Player.gameObject.GetComponentInChildren<Camera>();
        GameObject Path = gameObject.GetComponentInParent<Path>().Route;
        Points = Path.GetComponentsInChildren<Transform>();

        GoToNextPoint();
    }

    public void OnPlayerSpotted(GameObject gameObject)
    {
        IsPlayerSpotted = true;
        FollowPlayer();
    }

    public void OnPlayerLost(GameObject gameObject)
    {
        IsPlayerSpotted = false;
        GoToNextPoint();
    }

    private void FollowPlayer()
    {
        Vector3 Player = Camera.transform.position;
        Agent.SetDestination(Player);
    }

    private void GoToNextPoint()
    {
        if (Points.Length == 0)
            return;

        if (DestPos == 0)
            DestPos++;

        Agent.SetDestination(Points[DestPos].position);
        DestPos = (DestPos + 1) % Points.Length;
    }
}
