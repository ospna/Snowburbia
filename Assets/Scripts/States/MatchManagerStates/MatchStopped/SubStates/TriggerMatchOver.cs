using Assets.Scripts.StateMachines.Managers;
using Assets.Scripts.States.MatchManagerStates.MatchOver;
using RobustFSM.Base;
using RobustFSM.Interfaces;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates
{
    public class TriggerMatchOver : BState
    {
        public override void Enter()
        {
            base.Enter();

            //go to Match Over
            SuperFSM.ChangeState<MatchOverMainState>();
        }

        // Access the super state machine
        public IFSM SuperFSM
        {
            get
            {
                return (MatchManagerFSM)SuperMachine;
            }
        }

    }
}
