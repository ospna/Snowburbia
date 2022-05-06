using System;
using Assets.Scripts.Entities;
using static Assets.Scripts.Entities.Player;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
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

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            //if team is in control, raise the event that I'm chasing ball
            if (Owner.IsTeamInControl)
            {
                ChaseBallDel chase = Owner.OnChaseBall;
                if (chase != null)
                {
                    chase.Invoke(Owner);
                }
            }
            else
            {
                ChaseBallDel chase = Owner.OnChaseBall;
                if (chase != null)
                {
                    chase.Invoke(Owner);
                }
            }

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);
        }

        public override void Exit()
        {
            base.Exit();

            // deregister from listening to some events
            Owner.OnIsNoLongerClosestPlayerToBall -= Instance_OnIsNoLongerClosestPlayerToBall;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner._animator.SetBool("isJogging", false);
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
