using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.InterceptShot;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState;
using static GoalTrigger;
using RobustFSM.Base;
using RobustFSM.Interfaces;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal
{
    // The keeper protects the goal from the opposition
    public class ProtectGoalMainState : BState
    {
        int _goalLayerMask;
        float _timeSinceLastUpdate;
        Vector3 _steeringTarget;
        Vector3 _prevBallPosition;

        public override void Enter()
        {
            base.Enter();

            _goalLayerMask = LayerMask.GetMask("GoalTrigger");

            //set some data
            _prevBallPosition = 1000 * Vector3.one;
            _timeSinceLastUpdate = 0.05f;

            //set the rpg movement
            Owner.RPGMovement.SetSteeringOn();
            Owner.RPGMovement.Speed = Owner.TendGoalSpeed;
            Owner.TendGoalDistance = 1.5f;

            //register to some events
            Owner.OnShotTaken += Instance_OnShotTaken;
        }

        public override void Execute()
        {
            base.Execute();

            //get the ball position
            Vector3 ballPosition = Ball.Instance.NormalizedPosition;

            //set the look target
            Owner.RPGMovement.SetRotateFacePosition(ballPosition);

            //if I have exhausted my time then update the tend point
            if (_timeSinceLastUpdate <= 0f)
            {
                //do not continue if the ball didnt move
                if (_prevBallPosition != ballPosition)
                {
                    //cache the ball position
                    _prevBallPosition = ballPosition;

                    //run the logic for protecting the goal, find the position
                    Vector3 ballRelativePosToGoal = Owner.TeamGoal.transform.InverseTransformPoint(ballPosition);
                    ballRelativePosToGoal.z = Owner.TendGoalDistance;
                    ballRelativePosToGoal.x /= 2f;
                    ballRelativePosToGoal.x = Mathf.Clamp(ballRelativePosToGoal.x, -2f, 2f);
                    _steeringTarget = Owner.TeamGoal.transform.TransformPoint(ballRelativePosToGoal);

                    //add some noise to the target
                    float limit = 1f - Owner.GoalKeeping;
                    _steeringTarget.x += Random.Range(-limit, limit);
                    _steeringTarget.z += Random.Range(-limit, limit);
                }

                //reset the time 
                _timeSinceLastUpdate = 1f * (1f - Owner.GoalKeeping);

                if (_timeSinceLastUpdate == 0f)
                {
                    _timeSinceLastUpdate = 1f * 0.15f;
                }
            }

            //decrement the time
            _timeSinceLastUpdate -= Time.deltaTime;

            //set the ability to steer here
            Owner.RPGMovement.Steer = Vector3.Distance(Owner.Position, _steeringTarget) >= .05f;
            Owner.RPGMovement.SetMoveTarget(_steeringTarget);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            if (Owner.IsTeamInControl == true)
            {
                SuperMachine.ChangeState<GoToHomeMainState>();
            }
        }

        public override void Exit()
        {
            base.Exit();

            Owner.OnShotTaken -= Instance_OnShotTaken;
        }

        private void Instance_OnShotTaken(float flightTime, float velocity, Vector3 initial, Vector3 target)
        {
            // get the direction to target
            Vector3 direction = target - initial;

            // make a raycast and test if it hits target
            RaycastHit hitInfo;
            bool willBallHitAGoal = Physics.SphereCast(Ball.Instance.NormalizedPosition,
                        Ball.Instance.SphereCollider.radius, direction, out hitInfo, 500, _goalLayerMask);
            
            // get the goal from the goal trigger
            if (willBallHitAGoal)
            {
                // get the goal
                Goal goal = hitInfo.transform.GetComponent<GoalTrigger>().Goal;

                // check if shot is on target
                bool isShotOnTarget = goal == Owner.TeamGoal;

                if (isShotOnTarget == true)
                {
                    // init the intercept shot state
                    InterceptShotMainState interceptShotState = Machine.GetState<InterceptShotMainState>();
                    interceptShotState.BallInitialPosition = initial;
                    interceptShotState.BallInitialVelocity = velocity;
                    interceptShotState.ShotTarget = target;

                    // trigger state change
                    Machine.ChangeState<InterceptShotMainState>();
                }
            }
        }

        public Player Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }
    }
}
