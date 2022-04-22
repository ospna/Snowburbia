using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.Team.KickOff.KickOffMainState;
using Assets.Scripts.Utilities;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.Team.Wait
{
    public class WaitMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            //listen to some team events
            Owner.OnMessagedToTakeKickOff += Instance_OnMessagedSwitchToTakeKickOff;

            // raise the team wait event
            ActionUtility.Invoke_Action(Owner.OnInstructPlayersToWait);

        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to some team events
            Owner.OnMessagedToTakeKickOff -= Instance_OnMessagedSwitchToTakeKickOff;
        }

        private void Instance_OnMessagedSwitchToTakeKickOff()
        {
            Machine.ChangeState<KickOffMainState>();
        }

        public Assets.Scripts.Entities.Team Owner
        {
            get
            {
                return ((TeamFSM)SuperMachine).Owner;
            }
        }
    }
}
