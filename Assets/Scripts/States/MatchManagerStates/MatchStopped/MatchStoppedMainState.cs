using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates;
using Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates;
using Assets.Scripts.Utilities;
using RobustFSM.Base;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.MainState
{
    public class MatchStoppedMainState : BHState
    {
        public override void AddStates()
        {
            //add the states
            AddState<CheckNextMatchStatus>();
            AddState<BroadcastGoalScored>();
            AddState<BroadcastHalfTimeStatus>();
            AddState<HalfTime>();
            AddState<SwitchSides>();
            AddState<TriggerMatchOver>();

            //set the initial state
            SetInitialState<CheckNextMatchStatus>();
        }

        public override void Enter()
        {
            base.Enter();

            //register the teams to listen to the take-off events
            Owner.OnBroadcastTakeKickOff += Owner.TeamAway.OnMessagedToTakeKickOff;
            Owner.OnBroadcastTakeKickOff += Owner.TeamHome.OnMessagedToTakeKickOff;

            //raise the match stopped event
            ActionUtility.Invoke_Action(Owner.OnStopMatch);
        }

        public override void Exit()
        {
            base.Exit();

            //deregister the teams to listen to the take-off events
            Owner.OnBroadcastTakeKickOff -= Owner.TeamAway.OnMessagedToTakeKickOff;
            Owner.OnBroadcastTakeKickOff -= Owner.TeamHome.OnMessagedToTakeKickOff;
        }

        // Returns the owner of this instance
        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
