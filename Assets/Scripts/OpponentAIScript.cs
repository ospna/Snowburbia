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
        midfielder,
        attacker,
        keepPossesion
    }

    opponentStates currentState;

    public GameObject opponentGoal;

    // Start is called before the first frame update
    void Start()
    {
        opponentAgent = GetComponent<NavMeshAgent>();
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
            case opponentStates.midfielder:
                {
                    break;
                }
            case opponentStates.attacker:
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
