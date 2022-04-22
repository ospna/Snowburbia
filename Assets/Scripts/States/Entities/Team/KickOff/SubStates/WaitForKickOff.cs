﻿using System;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.Team.Defend;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.Team.KickOff.SubStates
{
    public class WaitForKickOff : BState
    {
        public override void Enter()
        {
            base.Enter();

            //listen to opponent ontake-kick-off event
            Owner.Opponent.OnTakeKickOff += Instance_OnOpponentTakeKickOff;
        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to opponent ontake-kick-off event
            Owner.Opponent.OnTakeKickOff -= Instance_OnOpponentTakeKickOff;
        }

        private void Instance_OnOpponentTakeKickOff()
        {
            SuperMachine.ChangeState<DefendMainState>();
        }

        public Scripts.Entities.Team Owner
        {
            get
            {
                return ((TeamFSM)SuperMachine).Owner;
            }
        }
    }
}