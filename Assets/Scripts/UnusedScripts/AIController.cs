using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] protected AIData aiData;

    [SerializeField] protected OpponentAIScript aiScript;

    private void Idle() => RunAgent(transform.position, 0.0f);

    private void Roam() => RunAgent(RandomNavMeshLocation(), aiData.roamSpeed);

    private void Pressure() => RunAgent(aiData.target.transform.position, aiData.pressureSpeed);

    private void BackToPos() => RunAgent(aiData.startPos, aiData.sprintSpeed);

    private void WatchBall() => RunAgent(aiData.target.transform.position, aiData.pressureSpeed);

    private void KeepPossession() { } //=> RunAgent(RandomNavMeshLocation(), aiData.roamSpeed);

    private void Tackle() => RunAgent(aiData.target.transform.position, aiData.pressureSpeed);

    private void Pass() { } //=> RunAgent(RandomNavMeshLocation(), aiData.roamSpeed);

    private void TakeShot() => RunAgent(aiData.opponentGoal, aiData.sprintSpeed);

    private void Defend() { } //  => RunAgent(RandomNavMeshLocation(), aiData.roamSpeed);

    private void SaveShot() { } // => RunAgent(RandomNavMeshLocation(), aiData.roamSpeed);


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
                aiScript.transform.LookAt(aiData.target.transform.position);
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
