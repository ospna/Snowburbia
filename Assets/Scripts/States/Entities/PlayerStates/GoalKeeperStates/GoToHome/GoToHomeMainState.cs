﻿using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState
{
    public class GoToHomeMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add the states
            AddState<SteerToHome>();
            AddState<WaitAtHome>();

            //set the initial state
            SetInitialState<SteerToHome>();
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            // run logic depending on whether team is in control or not
            if (Owner.IsTeamInControl == false)
                SuperMachine.ChangeState<ProtectGoalMainState>();
        }

        public Player Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }
    }
}