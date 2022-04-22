using System;
using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines.Managers;
using Assets.Scripts.States.MatchManagerStates.MatchOn;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.Init.SubStates
{
    public class WaitForMatchOnInstruction : BState
    {
        public override void Enter()
        {
            base.Enter();

            //listen to some events
            Owner.OnMesssagedToSwitchToMatchOn += Instance_OnMessagedToSwitchToMatchOn;

            //raise the event that I'm waiting for the match on instruction
            ActionUtility.Invoke_Action(Owner.OnEnterWaitForMatchOnInstruction);
        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to some events
            Owner.OnMesssagedToSwitchToMatchOn -= Instance_OnMessagedToSwitchToMatchOn;

            //raise the event that I have exited the match on instruction
            ActionUtility.Invoke_Action(Owner.OnExitWaitForMatchOnInstruction);
        }

        private void Instance_OnMessagedToSwitchToMatchOn()
        {
            SuperMachine.ChangeState<MatchOnMainState>();
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
