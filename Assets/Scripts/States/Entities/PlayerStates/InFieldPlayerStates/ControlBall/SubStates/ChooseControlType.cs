using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.SubStates
{
    public class ChooseControlType : BState
    {
        public override void Enter()
        {
            base.Enter();

            if (Owner.IsUserControlled)
                Machine.ChangeState<ManualControl>();
            else
                Machine.ChangeState<AutomaticControl>();
        }

        public Player Owner
        {
            get
            {
                return ((InFieldPlayerFSM)SuperMachine).Owner;
            }
        }
    }
}
