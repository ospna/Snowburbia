using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.Wait;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.Init
{
    public class InitMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            // disable the user controlled icon
            Owner.IconUserControlled.SetActive(false);

            //init
            Owner.Init();

            //listen to some events
            Owner.OnInstructedToWait += Instance_OnWait;
        }

        private void Instance_OnWait()
        {
            Machine.ChangeState<WaitMainState>();
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
