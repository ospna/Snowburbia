using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentAIScript : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent opponentAgent;

    [SerializeField]
    enum opponentStates
    {
        goalkeeper,
        defenders,
        midfielders,
        attackers,
        keepPossesion
    }

    opponentStates currentState;

    public GameObject opponentGoal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case opponentStates.goalkeeper:
                {
                    break;
                }
            case opponentStates.defenders:
                {
                    break;
                }
            case opponentStates.midfielders:
                {
                    break;
                }
            case opponentStates.attackers:
                {
                    opponentAgent.SetDestination(opponentGoal.transform.position);
                    break;
                }
            case opponentStates.keepPossesion:
                {
                    break;
                }
        }
    }
}
