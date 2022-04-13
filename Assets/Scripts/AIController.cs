using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] protected AIData aiData;

    private void Idle() { }

    private void Roam() { }

    private void Pressure() { }

    private void BackToPos() { }

    private void WatchBall() { }

    private void KeepPossession() { }

    private void Tackle() { }

    private void Pass() { }

    private void TakeShot() { }

    private void Defend() { }

    private void SaveShot() { }


    protected void SetState(AIState state)
    {
        aiData.nextState = state;
        if(aiData.nextState != aiData.currentState)
        {
            aiData.currentState = aiData.nextState;
        }
    }

    protected void RunState()
    {
        switch(aiData.currentState)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Roam:
                Roam();
                break;
            case AIState.Pressure:
                Pressure();
                break;
            case AIState.BackToPos:
                BackToPos();
                break;
            case AIState.WatchBall:
                WatchBall();
                break;
            case AIState.KeepPossession:
                KeepPossession();
                break;
            case AIState.Tackle:
                Tackle();
                break;
            case AIState.Pass:
                Pass();
                break;
            case AIState.TakeShot:
                TakeShot();
                break;
            case AIState.Defend:
                Defend();
                break;
            case AIState.SaveShot:
                SaveShot();
                break;
        }
    }

    private void RunAgent(Vector3 destination, float speed)
    {
        if(aiData.opponentAgent != null && aiData.opponentAgent.remainingDistance <= aiData.opponentAgent.stoppingDistance)
        {
            aiData.opponentAgent.speed = speed;
            aiData.opponentAgent.SetDestination(destination);
        }
    }

    protected Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPos = Vector3.zero;
        Vector3 randomPos = Random.insideUnitSphere * aiData.roamRadius;
        randomPos += transform.position;

        if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, aiData.roamRadius, 1))
        {
            finalPos = hit.position;
        }
        return finalPos;
    }
}
