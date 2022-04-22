using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates
{
    public class ChooseChaseType : BState
    {
        public override void Enter()
        {
            base.Enter();

            if (Owner.IsUserControlled)
                Machine.ChangeState<ManualChase>();
            else
                Machine.ChangeState<AutomaticChase>();
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
