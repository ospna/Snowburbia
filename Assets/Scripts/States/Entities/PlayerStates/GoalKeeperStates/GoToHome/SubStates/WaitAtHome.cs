using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.SubStates
{
    public class WaitAtHome : BState
    {
        public override void Enter()
        {
            base.Enter();

            //stop steering
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOn();
        }

        public override void Execute()
        {
            base.Execute();

            //update the track position
            Owner.RPGMovement.SetRotateFacePosition(Ball.Instance.NormalizedPosition);

            if (Owner.IsBallWithinControlableDistance())
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

            //steer if not at target
            if (!Owner.IsAtTarget(Owner.HomeRegion.position))
                Machine.ChangeState<SteerToHome>();
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
