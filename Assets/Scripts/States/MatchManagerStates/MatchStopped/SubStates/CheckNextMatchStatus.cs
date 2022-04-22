using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates;
using Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates;
using Assets.Scripts.Utilities.Enums;
using RobustFSM.Base;
using RobustFSM.Interfaces;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates
{
    public class CheckNextMatchStatus : BState
    {
        public override void Enter()
        {
            base.Enter();

            // run the logic depending on match status
            if (Owner.MatchStatus == MatchStatuses.GoalScored)
            {
                Machine.ChangeState<BroadcastGoalScored>();
            }
            else if (Owner.MatchStatus == MatchStatuses.HalfExhausted)
            {
                //if it's the first half then we have to switch sides
                if (Owner.CurrentHalf == 1)
                {
                    Machine.ChangeState<BroadcastHalfTimeStatus>();
                }
                else if (Owner.CurrentHalf == 2)
                {
                    Machine.ChangeState<TriggerMatchOver>();
                }
            }
        }

        // Access the super state machine
        public IFSM SuperFSM
        {
            get
            {
                return (MatchManagerFSM)SuperMachine;
            }
        }

        // Access the owner of the state machine
        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
