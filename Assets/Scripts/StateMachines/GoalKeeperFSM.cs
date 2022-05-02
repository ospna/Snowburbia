using Assets.RobustFSM.Mono;
using Assets.Scripts.Entities;
using UnityEngine;
using System;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.Init;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.InterceptShot;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.Wait;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState;

namespace Assets.Scripts.StateMachines
{
    public class GoalKeeperFSM : MonoFSM<Player>
    {
        public override void AddStates()
        {
            base.AddStates();

            //set the manual execute time
            SetUpdateFrequency(0.5f);

            // add states
            AddState<GoToHomeMainState>();
            AddState<InitMainState>();
            AddState<InterceptShotMainState>();
            AddState<ProtectGoalMainState>();
            AddState<WaitMainState>();

            // set initial states
            SetInitialState<InitMainState>();
        }
    }
}
