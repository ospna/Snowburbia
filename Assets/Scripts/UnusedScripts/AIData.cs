using UnityEngine;
using UnityEngine.AI;

public enum AIState { Idle, Roam, Pressure, BackToPos, WatchBall, KeepPossession, Tackle, Pass, TakeShot, Defend, SaveShot }

[System.Serializable]
public struct AIData
{
    [SerializeField]
    public NavMeshAgent opponentAgent;
    public Transform target;
    public Vector3 startPos;
    public Vector3 opponentGoal;
    public Vector3 ownGoal;
    public AIState currentState;
    [HideInInspector] public AIState nextState;

    [Range(0, 10)] public float roamSpeed;
    [Range(0, 10)] public float sprintSpeed;
    [Range(0, 10)] public float pressureSpeed;
    [Range(0, 10)] public float tackleSpeed;

    [Range(1, 50)] public float roamRadius;

    [Range(0, 10)] public float minPressureDistance;
    [Range(0, 10)] public float maxPressureDistance;
    [Range(0, 10)] public float minTackleDistance;
    [Range(0, 10)] public float setBackToPosDistance;

    public bool goBackToPos;
}
