using System;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Wait;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Init
{
    public class InitMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            // disable the user controlled icon
            Owner.IconUserControlled.SetActive(false);

            //init
            Owner.Init();

            //listen to some events
            Owner.OnInstructedToWait += Instance_OnWait;
        }

        private void Instance_OnWait()
        {
            Machine.ChangeState<WaitMainState>();
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
