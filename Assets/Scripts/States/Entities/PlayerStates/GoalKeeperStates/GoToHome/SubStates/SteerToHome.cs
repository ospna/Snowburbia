using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.SubStates
{
    public class SteerToHome : BState
    {
        public Vector3 SteeringTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            //get the steering target
            SteeringTarget = Owner.HomeRegion.position;

            //set the steering to on
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
            Owner.RPGMovement.SetSteeringOn();
            Owner.RPGMovement.SetTrackingOn();
        }


        public override void Execute()
        {
            base.Execute();

            //check if now at target and switch to wait for ball
            if (Owner.IsAtTarget(SteeringTarget))
                Machine.ChangeState<WaitAtHome>();

            if (Owner.IsBallWithinControllableDistance())
            {
                // find direction to deflect ball to
                Vector3 localPoint = Owner.TeamGoal.transform.InverseTransformPoint(Owner.Position);
                localPoint.y = localPoint.z = 0f;

                // find the direction in world space
                Vector3 direction = Owner.TeamGoal.transform.TransformPoint(localPoint);

                // deflect ball
                Ball.Instance.Kick(Owner.Position + direction.normalized, Ball.Instance.Rigidbody.velocity.magnitude * -1.5f);
            }
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            //update the steering target
            SteeringTarget = Owner.HomeRegion.position;

            //update the rpg movement
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
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
