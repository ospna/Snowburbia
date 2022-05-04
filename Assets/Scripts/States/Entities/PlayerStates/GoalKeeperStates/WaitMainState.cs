using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.Wait
{
    public class WaitMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            // stop steering
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOff();

            //listen to variaus events
            Owner.OnInstructedToGoToHome += Instance_OnInstructedToGoToHome;

            if (Owner.IsBallWithinControllableDistance())
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

            //stop listening to variaus events
            Owner.OnInstructedToGoToHome -= Instance_OnInstructedToGoToHome;
        }

        public void Instance_OnInstructedToGoToHome()
        {
            Machine.ChangeState<GoToHomeMainState>();
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
