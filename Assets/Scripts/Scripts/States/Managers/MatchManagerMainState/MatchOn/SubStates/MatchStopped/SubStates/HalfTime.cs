using System;
using Assets.Scripts.Managers;
using static Assets.Scripts.Managers.MatchManager;
using Assets.SoccerGameEngine_Basic_.Scripts.StateMachines.Managers;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.SoccerGameEngine_Basic_.Scripts.States.Managers.MatchManagerMainState.MatchOn.SubStates.MatchStopped.SubStates
{
    public class HalfTime : BState
    {
        public override void Enter()
        {
            base.Enter();

            //listen to instructions to go to second half
            Owner.OnContinueToSecondHalf += Instance_OnContinueToSecondHalf;

            //raise the event that I have entered the half-time state
            RaiseTheHalfTimeStartEvent();

        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to instructions to go to second half
            Owner.OnContinueToSecondHalf -= Instance_OnContinueToSecondHalf;

            //raise the event that I have exited the half-time state
            ActionUtility.Invoke_Action(Owner.OnExitHalfTime);

        }

        /// <summary>
        /// Raises the half start event
        /// </summary>
        public void RaiseTheHalfTimeStartEvent()
        {
            //prepare an empty string
            //string message = string.Format("Team Away {0}-{1} Team Home", Owner.TeamAway.Goals, Owner.TeamHome.Goals);

            string message = string.Format("Home: " + Owner.TeamHome.Goals + " Away: " + Owner.TeamAway.Goals);

            //raise the event
            EnterHalfTime temp = Owner.OnEnterHalfTime;
            if (temp != null) temp.Invoke(message);
        }

        private void Instance_OnContinueToSecondHalf()
        {
            Machine.ChangeState<SwitchSides>();
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
