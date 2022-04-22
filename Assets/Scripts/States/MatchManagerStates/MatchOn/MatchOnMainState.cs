using System;
using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines.Managers;
using Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates;
using Assets.Scripts.States.MatchManagerStates.MatchStopped.MainState;
using RobustFSM.Base;

namespace Assets.Scripts.States.MatchManagerStates.MatchOn
{
    public class MatchOnMainState : BHState
    {
        public override void AddStates()
        {
            //add the states
            AddState<BroadcastHalfStatus>();
            AddState<BroadcastMatchStatus>();
            AddState<ExhaustHalf>();
            AddState<MatchStoppedMainState>();
            AddState<WaitForKickOffToComplete>();

            //set the inital state
            SetInitialState<BroadcastMatchStatus>();
        }

        /// <summary>
        /// On enter
        /// </summary>
        public override void Enter()
        {
            base.Enter();

            //configure the teams to listen to some MatchManaher events
            Owner.OnStopMatch += Owner.TeamAway.Invoke_OnMessagedToStop;
            Owner.OnStopMatch += Owner.TeamHome.Invoke_OnMessagedToStop;

        }

        /// <summary>
        /// Returns the owner of this instance
        /// </summary>
        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
