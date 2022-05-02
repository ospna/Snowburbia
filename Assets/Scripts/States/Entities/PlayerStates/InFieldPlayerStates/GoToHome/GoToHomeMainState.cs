using Assets.RobustFSM.Interfaces;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.ChaseBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.PickOutThreat.PickOutThreatMain;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ReceiveBall.ReceiveBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SupportAttackerMain;
using RobustFSM.Base;
using RobustFSM.Interfaces;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState
{
    public class GoToHomeMainState : BHState
    {
        PickOutThreatMainState _pickOutThreatMainState;
        SupportAttackerMainState _supportAttackerMainState;

        public override void AddStates()
        {
            base.AddStates();

            //add the states
            AddState<SteerToHome>();
            AddState<WaitAtHome>();

            //set the initial state
            SetInitialState<SteerToHome>();
        }

        public override void Initialize()
        {
            base.Initialize();

            // set the initial states
            _pickOutThreatMainState = SuperMachine.GetState<PickOutThreatMainState>();
            _supportAttackerMainState = SuperMachine.GetState<SupportAttackerMainState>();
        }

        public override void Enter()
        {
            base.Enter();

            //listen to variaus events
            Owner.OnBecameTheClosestPlayerToBall += Instance_OnBecameTheClosestPlayerToBall;
            Owner.OnInstructedToReceiveBall += Instance_OnInstructedToReceiveBall;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            // run logic depending on whether team is in control or not
            if (Owner.IsTeamInControl)
            {
                // look ahead for a support spot, if support spot is available then I
                // can go into support attacket state
                _supportAttackerMainState.FindSupportSpot();

                // get the support spot from the main state
                //if no support spot then go to home
                SupportSpot supportSpot = _supportAttackerMainState.SupportSpot;

                // if we have a support spot go to support attacker main state
                if (supportSpot != null)
                    Machine.ChangeState<SupportAttackerMainState>();
            }
            else
            {
                // find the threat
                _pickOutThreatMainState.FindThreat();

                //get the threat from the state
                Player threat = _pickOutThreatMainState.Threat;

                // go to pick out threat state
                if (threat != null)
                    Machine.ChangeState<PickOutThreatMainState>();
            }

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);
        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to variaus events
            Owner.OnBecameTheClosestPlayerToBall -= Instance_OnBecameTheClosestPlayerToBall;
            Owner.OnInstructedToReceiveBall -= Instance_OnInstructedToReceiveBall;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner._animator.SetBool("isJogging", false);
        }

        private void Instance_OnBecameTheClosestPlayerToBall()
        {
            Machine.ChangeState<ChaseBallMainState>();
        }

        private void Instance_OnInstructedToReceiveBall(float ballTime, Vector3 position)
        {
            //get the receive ball state and init the steering target
            Machine.GetState<ReceiveBallMainState>().SetSteeringTarget(ballTime, position);
            Machine.ChangeState<ReceiveBallMainState>();
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
