using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
//using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.MainState;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using RobustFSM.Base;

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
        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to variaus events
            Owner.OnInstructedToGoToHome -= Instance_OnInstructedToGoToHome;
        }

        public void Instance_OnInstructedToGoToHome()
        {
            Machine.ChangeState<ProtectGoalMainState>();
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
