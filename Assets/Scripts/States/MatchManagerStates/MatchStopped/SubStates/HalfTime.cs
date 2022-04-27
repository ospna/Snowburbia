using System;
using Assets.Scripts.Managers;
using static Assets.Scripts.Managers.MatchManager;
using Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates
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

        // Raises the half start event
        public void RaiseTheHalfTimeStartEvent()
        {

            string message = string.Format("Home: " + Owner.TeamHome.Goals + " Away: " + Owner.TeamAway.Goals);

            //raise the event
            EnterHalfTime temp = Owner.OnEnterHalfTime;
            if (temp != null) temp.Invoke(message);
        }

        private void Instance_OnContinueToSecondHalf()
        {
            Machine.ChangeState<SwitchSides>();
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
