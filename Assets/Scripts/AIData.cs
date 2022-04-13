using UnityEngine;
using UnityEngine.AI;

public enum AIState { Idle, Roam, Pressure, BackToPos, WatchBall, KeepPossession, Tackle, Pass, TakeShot, Defend, SaveShot }

[System.Serializable]
public struct AIData
{
    [SerializeField]
    public NavMeshAgent opponentAgent;
    public Transform target;
    public AIState currentState;
    [HideInInspector] public AIState nextState;

    [Range(0, 100)] public float roamSpeed;
    [Range(0, 100)] public float sprintSpeed;
    [Range(0, 100)] public float pressureSpeed;
    [Range(0, 100)] public float tackleSpeed;

    [Range(1, 500)] public float roamRadius;

    [Range(0, 100)] public float minPressureDistance;
    [Range(0, 100)] public float maxPressureDistance;
    [Range(0, 100)] public float minTackleDistance;
    [Range(0, 100)] public float setBackToPosDistance;

    public bool goBackToPos;
}
