using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpponentAIScript : AIController
{
    //public Transform ballTransform;
    public GameObject ball;
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

    public Vector3 KickTarget;

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
        aiData.target = GameObject.FindGameObjectWithTag("SoccerBall").transform;
        aiData.startPos = transform.position;
        aiData.opponentGoal = GameObject.FindGameObjectWithTag("HomeInsideBox").transform.position;

        if (aiData.opponentAgent != null)
        {
            SetState(AIState.Roam);
            //aiData.opponentAgent.speed = aiData.roamSpeed;
            //aiData.opponentAgent.SetDestination(RandomNavMeshLocation());
        }
        //startingPosition = transform.position;
        //roamPosition = GetRoamingPosition();
        //aiHasball = sb.aiPlayerHasBall;

    }

    private void Update()
    {
        //ballInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBall);
        //ballInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBall);

        float distance = Vector3.Distance(aiData.target.transform.position, transform.position);

        if (distance > aiData.maxPressureDistance)
        {
            if (aiData.goBackToPos == true)
            {
                aiData.goBackToPos = false;
                SetState(AIState.Roam);
            }
        }

        if (distance <= aiData.maxPressureDistance && aiData.goBackToPos == false)
        {
            SetState(AIState.WatchBall);

            if (distance <= aiData.minTackleDistance)
            {
                SetState(AIState.Tackle);
            }

            if (distance < aiData.setBackToPosDistance)
            {
                aiData.goBackToPos = true;
                SetState(AIState.BackToPos);
            }
        }

        RunState();
    }
}
    /*
    /// <summary>
    /// Checks whether a player can pass
    /// </summary>
    /// <returns></returns>
    /// ToDo::Implement logic to cache players to message so that they can intercept the pass
    public bool CanPass(bool considerPassSafety = true)
    {
        //set the pass target
        bool passToPlayerClosestToMe = false;// Random.value <= 0.1f;

        //set the pass target
        KickTarget = null;

        //loop through each team player and find a pass for each
        foreach (OpponentAIScript player in TeamMembers)
        {
            // can't pass to myself
            bool isPlayerMe = player == this;
            if (isPlayerMe)
                continue;

            // we don't want to pass to the last receiver
            bool isPlayePrevPassReceiver = player == _prevPassReceiver;
            if (isPlayePrevPassReceiver)
                continue;

            // can't pass to the goalie
            bool isPlayerGoalKeeper = player.PlayerType == PlayerTypes.Goalkeeper;
            if (isPlayerGoalKeeper)
                continue;

            // check if player can pass
            CanPass(player.Position, considerPassSafety, passToPlayerClosestToMe, player);
        }

        //return result
        //Player can pass if there is a pass target
        return KickTarget != null;
    }

    public bool CanPass(Vector3 position, bool considerPassSafety = true, bool considerPlayerClosestToMe = false, Player player = null)
    {
        //get the possible pass options
        List<Vector3> passOptions = GetPassPositionOptions(position);

        //loop through each option and search if it is possible to 
        //pass to it. Consider positions higher up the pitch
        foreach (Vector3 passOption in passOptions)
        {
            // check if position is within pass range
            bool isPositionWithinPassRange = IsPositionWithinPassRange(passOption);

            // we consider a target which is out of our min pass distance
            if (isPositionWithinPassRange == true)
            {
                //find power to kick ball
                float power = FindPower(Ball.Instance.NormalizedPosition,
                    passOption,
                    BallPassArriveVelocity,
                    Ball.Instance.Friction);

                //clamp the power to the player's max power
                power = Mathf.Clamp(power, 0f, this.ActualPower);

                //find if ball can reach point
                float ballTimeToTarget = 0f;
                bool canBallReachTarget = CanBallReachPoint(passOption,
                        power,
                        out ballTimeToTarget);

                //return false if the time is less than zero
                //that means the ball can't reach it's target
                if (canBallReachTarget == false)
                    return false;

                // get time of player to point
                float timeOfReceiverToTarget = TimeToTarget(position,
                    passOption,
                    ActualSpeed);

                // pass is not safe if receiver can't reach target before the ball
                if (timeOfReceiverToTarget > ballTimeToTarget)
                    return false;

                // check if pass is safe from all opponents
                bool isPassSafeFromAllOpponents = false;
                if (considerPassSafety)
                {
                    // check pass safety
                    isPassSafeFromAllOpponents = IsPassSafeFromAllOpponents(Ball.Instance.NormalizedPosition,
                        position,
                        passOption,
                        power,
                        ballTimeToTarget);
                }

                //if pass is safe from all opponents then cache it
                if (considerPassSafety == false ||
                    (considerPassSafety == true && isPassSafeFromAllOpponents == true))
                {
                    if (considerPlayerClosestToMe)
                    {
                        //set the pass-target to be the initial position
                        //check if pass is closer to goal and save it
                        if (KickTarget == null
                            || IsPositionCloserThanPosition(Position,
                                                    passOption,
                                                    (Vector3)KickTarget))
                        {
                            BallTime = ballTimeToTarget;
                            KickPower = power;
                            KickTarget = passOption;
                            PassReceiver = player;
                        }
                    }
                    else
                    {
                        //set the pass-target to be the initial position
                        //check if pass is closer to goal and save it
                        if (KickTarget == null
                            || IsPositionCloserThanPosition(OppGoal.transform.position,
                                                    passOption,
                                                    (Vector3)KickTarget))
                        {
                            BallTime = ballTimeToTarget;
                            KickPower = power;
                            KickTarget = passOption;
                            PassReceiver = player;
                        }
                    }
                }
            }
        }

        //return result
        //Player can pass if there is a pass target
        return KickTarget != null;
    }

    public bool CanPassInDirection(Vector3 direction)
    {
        //set the pass target
        bool passToPlayerClosestToMe = Random.value <= 0.75f;

        //set the pass target
        KickTarget = null;

        //loop through each team player and find a pass for each
        foreach (Player player in TeamMembers)
        {
            // find a pass to a player who isn't me
            // who isn't a goal keeper
            // who is in this direction
            if (player != this
                && player.PlayerType == PlayerTypes.InFieldPlayer
                && IsPositionInDirection(direction, player.Position, 22.5f))
            {
                CanPass(player.Position, true, passToPlayerClosestToMe, player);
            }
        }

        // if there is no pass simply find a team member in this direction
        if (KickTarget == null)
        {
            //loop through each team player and find a pass for each
            foreach (Player player in TeamMembers)
            {
                // find a pass to a player who isn't me
                // who isn't a goal keeper
                // who is in this direction
                if (player != this
                    && player.PlayerType == PlayerTypes.InFieldPlayer
                    && IsPositionInDirection(direction, player.Position, 22.5f))
                {
                    CanPass(player.Position, false, passToPlayerClosestToMe, player);
                }
            }
        }

        // if still there is no player simply find a player to pass to

        //return result
        //Player can pass if there is a pass target
        return KickTarget != null;

    }

    public bool IsPassSafeFromAllOpponents(Vector3 initialPosition, Vector3 receiverPosition, Vector3 target, float initialBallVelocity, float time)
    {
        //look for a player threatening the pass
        foreach (Player player in OppositionMembers)
        {
            bool isPassSafeFromOpponent = IsPassSafeFromOpponent(initialPosition,
                target,
                player.Position,
                receiverPosition,
                initialBallVelocity,
                time);

            //return false if the pass is not safe
            if (isPassSafeFromOpponent == false)
                return false;
        }

        //return result
        return true;
    }

    public bool IsPassSafeFromOpponent(Vector3 initialPosition, Vector3 target, Vector3 oppPosition, Vector3 receiverPosition, float initialBallVelocity, float timeOfBall)
    {
        #region Consider some logic that might threaten the pass

        //we might not want to pass to a player who is highly threatened(marked)
        if (IsPositionAHighThreat(receiverPosition, oppPosition))
            return false;

        //return false if opposition is closer to target than reciever
        if (IsPositionCloserThanPosition(target, oppPosition, receiverPosition))
            return false;

        //If oppossition is not between the passing lane then he is behind the passer
        //receiver and he can't intercept the ball
        if (IsPositionBetweenTwoPoints(initialPosition, receiverPosition, oppPosition) == false)
            return true;

        #endregion

        #region find if opponent can intercept ball

        //check if pass to position can be intercepted
        Vector3 orthogonalPoint = GetPointOrthogonalToLine(initialPosition,
            target,
            oppPosition);

        //get time of ball to point
        float timeOfBallToOrthogonalPoint = 0f;
        CanBallReachPoint(orthogonalPoint, initialBallVelocity, out timeOfBallToOrthogonalPoint);

        //get time of opponent to target
        float timeOfOpponentToTarget = TimeToTarget(oppPosition,
        orthogonalPoint,
        ActualSpeed);

        //ball is safe if it can reach that point before the opponent
        bool canBallReachOrthogonalPointBeforeOpp = timeOfBallToOrthogonalPoint < timeOfOpponentToTarget;

        if (canBallReachOrthogonalPointBeforeOpp == true)
            return true;
        else
            return false;
        // return true;
        #endregion
    }

    /// <summary>
    /// Checks whether this instance is picked out or not
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsPickedOut(Player player)
    {
        return SupportSpot.IsPickedOut(player);
    }

    public bool IsPositionBetweenTwoPoints(Vector3 A, Vector3 B, Vector3 point)
    {
        //find some direction vectors
        Vector3 fromAToPoint = point - A;
        Vector3 fromBToPoint = point - B;
        Vector3 fromBToA = A - B;
        Vector3 fromAToB = -fromBToA;

        //check if point is inbetween and return result
        return Vector3.Dot(fromAToB.normalized, fromAToPoint.normalized) > 0
            && Vector3.Dot(fromBToA.normalized, fromBToPoint.normalized) > 0;
    }

    /// <summary>
    /// Checks whether the first position is closer to target than the second position
    /// </summary>
    /// <param name="target"></param>
    /// <param name="position001"></param>
    /// <param name="position002"></param>
    /// <returns></returns>
    public bool IsPositionCloserThanPosition(Vector3 target, Vector3 position001, Vector3 position002)
    {
        return Vector3.Distance(position001, target) < Vector3.Distance(position002, target);
    }

    public bool IsPositionInDirection(Vector3 forward, Vector3 position, float angle)
    {
        // find direction to target
        Vector3 directionToTarget = position - Position;

        // find angle between forward and direction to target
        float angleBetweenDirections = Vector3.Angle(forward.normalized, directionToTarget.normalized);

        // return result
        return angleBetweenDirections <= angle / 2;
    }

    public bool IsPositionThreatened(Vector3 position)
    {
        //search for threatening player
        foreach (Player player in OppositionMembers)
        {
            if (IsPositionWithinHighThreatDistance(position, player.Position))
                return true;
        }

        //return false
        return false;

    }

    public bool IsPositionWithinMinPassDistance(Vector3 position)
    {
        return IsWithinDistance(Position,
            position,
            _distancePassMin);
    }

    public bool IsPositionWithinMinPassDistance(Vector3 center, Vector3 position)
    {
        return IsWithinDistance(center,
            position,
            _distancePassMin);
    }
}

            /*
            if (aiData.opponentAgent != null && aiData.opponentAgent.remainingDistance <= aiData.opponentAgent.stoppingDistance)
            {
                aiData.opponentAgent.SetDestination(RandomNavMeshLocation());
            }
            */

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