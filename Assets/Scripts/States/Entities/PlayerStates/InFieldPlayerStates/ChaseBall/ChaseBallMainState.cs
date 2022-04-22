﻿using System;
using Assets.Scripts.Entities;
using static Assets.Scripts.Entities.Player;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.MainState;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.ChaseBallMainState
{
    public class ChaseBallMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            // add states
            AddState<AutomaticChase>();
            AddState<ChooseChaseType>();
            AddState<ManualChase>();

            // set initial state
            SetInitialState<ChooseChaseType>();
        }

        public override void Enter()
        {
            base.Enter();

            // listen to player events
            Owner.OnIsNoLongerClosestPlayerToBall += Instance_OnIsNoLongerClosestPlayerToBall;
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            //if team is incontrol, raise the event that I'm chasing ball
            if (Owner.IsTeamInControl)
            {
                ChaseBallDel temp = Owner.OnChaseBall;
                if (temp != null)
                    temp.Invoke(Owner);
            }
        }

        public override void Exit()
        {
            base.Exit();

            // deregister from listening to some events
            Owner.OnIsNoLongerClosestPlayerToBall -= Instance_OnIsNoLongerClosestPlayerToBall;
        }

        private void Instance_OnIsNoLongerClosestPlayerToBall()
        {
            Machine.ChangeState<GoToHomeMainState>();
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