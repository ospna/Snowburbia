using System;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ReceiveBall.ReceiveBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TakeKickOff;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Wait
{
    // The player simply waits for an instruction and reacts to that
    public class WaitMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            // stop steering
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOff();

            //listen to variaus events
            Owner.OnInstructedToGoToHome += Instance_OnInstructedToGoToHome;
            Owner.OnInstructedToReceiveBall += Instance_OnInstructedToReceiveBall;
            Owner.OnInstructedToTakeKickOff += Instance_OnInstructedToTakeKickOff;
        }

        public override void Exit()
        {
            base.Exit();

            //stop listening to variaus events
            Owner.OnInstructedToGoToHome -= Instance_OnInstructedToGoToHome;
            Owner.OnInstructedToReceiveBall -= Instance_OnInstructedToReceiveBall;
            Owner.OnInstructedToTakeKickOff -= Instance_OnInstructedToTakeKickOff;
        }

        public void Instance_OnInstructedToGoToHome()
        {
            Machine.ChangeState<GoToHomeMainState>();
        }

        private void Instance_OnInstructedToReceiveBall(float ballTime, Vector3 position)
        {
            //get the receive ball state and init the steering target
            Machine.GetState<ReceiveBallMainState>().SetSteeringTarget(ballTime, position);
            Machine.ChangeState<ReceiveBallMainState>();
        }

        private void Instance_OnInstructedToTakeKickOff()
        {
            Machine.ChangeState<TakeKickOffMainState>();
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
