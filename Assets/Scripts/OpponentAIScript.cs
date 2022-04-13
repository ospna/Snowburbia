using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentAIScript : AIController
{
    //public Transform ballTransform;
    public GameObject ball;
    public GameObject home18;
    Rigidbody rigBod;

    bool aiHasball;
    private CharacterController aiController;

    SoccerBall sb;

    public LayerMask whatIsGround, whatIsBall;

    // Moving
    public Vector3 movePoint;
    bool movePointSet;
    public float movePointRange;

    // Attacking the ball
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange, radius;
    public bool ballInSightRange, ballInAttackRange;

    //private Vector3 startingPosition;
    //private Vector3 roamPosition;

    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("SoccerBall");
        //aiData.opponentAgent = GetComponent<NavMeshAgent>();
        //rigBod = SoccerBall.GetComponent<Rigidbody>();

        //currentState = opponentStates.roam;
    }

    private void Start()
    {
        aiData.opponentAgent = GetComponent<NavMeshAgent>();
        if (aiData.opponentAgent != null)
        {
            aiData.opponentAgent.speed = aiData.roamSpeed;
            aiData.opponentAgent.SetDestination(RandomNavMeshLocation());
        }
        ///startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
        //aiHasball = sb.aiPlayerHasBall;

    }

    private void Update()
    {
        //ballInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBall);
        //ballInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBall);

        if (aiData.opponentAgent != null && aiData.opponentAgent.remainingDistance <= aiData.opponentAgent.stoppingDistance)
        {
            aiData.opponentAgent.SetDestination(RandomNavMeshLocation());
        }

        /*
        switch (currentState)
        {
            default:
            case opponentStates.roam:
                {
                    if (!movePointSet)
                    {
                        SearchMovePoint();
                    }

                    if (movePointSet)
                    {
                        aiData.opponentAgent.SetDestination(movePoint);
                    }

                    Vector3 distanceToMovePoint = transform.position - movePoint;

                    // Move point was reached
                    if (distanceToMovePoint.magnitude < 1f)
                    {
                        movePointSet = false;
                    }

                    break;
                }
            case opponentStates.pressure:
                {
                    aiData.opponentAgent.destination = ball.transform.position;

                    break;
                }
            case opponentStates.watch:
                {
                    transform.LookAt(ball.transform.position);

                    break;
                }
            case opponentStates.possession:
                {
                    break;
                }
            case opponentStates.attacker:
                {
                    aiData.opponentAgent.SetDestination(home18.transform.position);
                    break;
                }
            case opponentStates.midfielder:
                {
                    break;
                }
            case opponentStates.defender:
                {
                    break;
                }
            case opponentStates.goalkeeper:
                {
                    break;
                }
        }


        if (!ballInSightRange && !ballInAttackRange)
        {
            currentState = opponentStates.roam;
        }

        if (ballInSightRange && !ballInAttackRange)
        {
            currentState = opponentStates.watch;
        }

        if (ballInSightRange && ballInAttackRange)
        {
            currentState = opponentStates.pressure;
        }

        //ScoreGoal();

        /*
        if (!aiData.opponentAgent.hasPath)
        {
            aiData.opponentAgent.SetDestination(GetPoint.Instance.GetRandomPoint(transform, radius));
        }
        */
        

    }

    /*
    private void FindTarget()
    {
        float targetRange = 50f;
        if(Vector3.Distance(transform.position, ball.transform.position) < targetRange)
        {
            currentState = opponentStates.pressure;
        }
    }
    */


    /*
    private void Moving()
    {
        //return startingPosition + GetRandomDir() * Random.Range(10f, 70f);

        if (!movePointSet)
        {
            SearchMovePoint();
        }

        if (movePointSet)
        {
            aiData.opponentAgent.SetDestination(movePoint);
        }

        Vector3 distanceToMovePoint = transform.position - movePoint;

        // Move point was reached
        if (distanceToMovePoint.magnitude < 1f)
        {
            movePointSet = false;
        }

        currentState = opponentStates.roam;

    }
    */

    /*
    private void SearchMovePoint()
    {
        // calculate random point in range
        float randomZ = Random.Range(-movePointRange, movePointRange);
        float randomX = Random.Range(-movePointRange, movePointRange);

        movePoint = new Vector3(transform.position.x + randomX, transform.position.y + transform.position.z + randomZ);

        if (Physics.Raycast(movePoint, -transform.up, 2f, whatIsGround))
        {
            movePointSet = true;
        }
    }

    /*
    private void ChaseBall()
    {
        aiData.opponentAgent.SetDestination(ball.transform.position);

    }
    */

        /*
    private void AttackBall()
    {
        transform.LookAt(ball.transform.position);

        if (!alreadyAttacked)
        {
            // attack code here

        }
            /*
            ball.transform.parent = this.AIholdBall.transform;
            transform.localPosition = new Vector3(0, 0.1f, 0.5f);
            transform.localRotation = Quaternion.identity;
            rigBod.velocity = Vector3.zero;
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }


    private void ScoreGoal()
    {
        if (aiHasball == true)
        {
           aiData.opponentAgent.SetDestination(home18.position);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
*/
    }
