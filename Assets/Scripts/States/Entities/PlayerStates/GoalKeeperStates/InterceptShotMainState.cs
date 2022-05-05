using System;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
//using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.InterceptShot
{
    public class InterceptShotMainState : BState
    {
        float timeOfBallToInterceptPoint;
        Vector3 _steerTarget;

        public float  BallInitialVelocity { get; set; }
        public Vector3 BallInitialPosition { get; set; }
        public Vector3 ShotTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            //find the point on the ball path to target that is orthogonal to player position
            _steerTarget = Owner.GetPointOrthogonalToLine(BallInitialPosition, ShotTarget, Owner.Position);

            // calculate time of ball to intercept point
            timeOfBallToInterceptPoint = Owner.TimeToTarget(BallInitialPosition, ShotTarget, BallInitialVelocity, Ball.Instance.Friction);

            // add some noise to it
            timeOfBallToInterceptPoint += 0.05f;

            if (Vector3.Distance(_steerTarget, ShotTarget) >= 0.05f)
                Owner.RPGMovement.SetSteeringOn();

            // set the steering 
            Owner.RPGMovement.SetMoveTarget(_steerTarget);
            Owner.RPGMovement.SetRotateFacePosition(BallInitialPosition);
            Owner.RPGMovement.SetTrackingOn();

            Owner.GetComponentInChildren<Animator>().SetBool("CanSave", true);
        }

        public override void Execute()
        {
            base.Execute();

            Owner.GetComponentInChildren<Animator>().SetBool("CanSave", true);
            Owner._animator.SetBool("CanSave", true);

            // keep steering to target
            Owner.RPGMovement.SetMoveTarget(_steerTarget);

            // decrement ball time
            timeOfBallToInterceptPoint -= Time.deltaTime;

            if(Vector3.Distance(_steerTarget, Owner.Position) <= 0.05f)
            {
                if (Owner.RPGMovement.Steer == true)
                    Owner.RPGMovement.SetSteeringOff();
            }

            // if ball within control distance te deflect its path
            if (timeOfBallToInterceptPoint <= 0f)
            {
                SuperMachine.ChangeState<ProtectGoalMainState>();
            }


            if(Owner.IsBallWithinControllableDistance())
            {
                // find direction to deflect ball to
                Vector3 localPoint = Owner.TeamGoal.transform.InverseTransformPoint(Owner.Position);
                localPoint.y = localPoint.z = 0f;

                // find the direction in world space
                Vector3 direction = Owner.TeamGoal.transform.TransformPoint(localPoint);

                // deflect ball
                Ball.Instance.Kick(Owner.Position + direction.normalized, Ball.Instance.Rigidbody.velocity.magnitude * -0.5f);
                
                // go to tend goal
                SuperMachine.ChangeState<ProtectGoalMainState>();
            }
        }

        public override void Exit()
        {
            base.Exit();

            Owner._animator.SetBool("CanSave", false);
            Owner.GetComponentInChildren<Animator>().SetBool("CanSave", false);

            // reset steering
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOff();
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
