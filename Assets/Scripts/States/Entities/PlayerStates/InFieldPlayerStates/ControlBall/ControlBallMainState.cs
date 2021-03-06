using System;
using UnityEngine;
using Assets.Scripts.Entities;
using static Assets.Scripts.Entities.Player;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Tackled;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState
{
    public class ControlBallMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add states
            AddState<AutomaticControl>();
            AddState<ChooseControlType>();
            AddState<ManualControl>();

            //set inital state
            SetInitialState<ChooseControlType>();
        }

        public override void Enter()
        {
            base.Enter();

            // set new speed
            Owner.RPGMovement.Speed *= 0.95f;

            //listen to game events
            Owner.OnTackled += Instance_OnTackled;

            //set the ball to is kinematic
            Ball.Instance.Owner = Owner;
            Ball.Instance.Rigidbody.isKinematic = true;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner.snowAnim.SetBool("isJogging", true);
            Owner.gingAnim.SetBool("isJogging", true);

            // raise event that I'm controlling the ball
            ControlBallDel temp = Owner.OnControlBall;
            if (temp != null)
                temp.Invoke(Owner);
        }

        public override void Execute()
        {
            base.Execute();

            //place ball infront of me
            Owner.PlaceBallInfrontOfMe();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner.snowAnim.SetBool("isJogging", true);
            Owner.gingAnim.SetBool("isJogging", true);
        }

        public override void Exit()
        {
            base.Exit();

            // restore player speed
            Owner.RPGMovement.Speed = Owner.ActualSpeed;

            //listen to game events
            Owner.OnTackled -= Instance_OnTackled;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner.snowAnim.SetBool("isJogging", false);
            Owner.gingAnim.SetBool("isJogging", false);

            //unset the ball to is kinematic
            Ball.Instance.Owner = null;
            Ball.Instance.Rigidbody.isKinematic = false;
        }

        public void Instance_OnTackled()
        {
            SuperMachine.ChangeState<TackledMainState>();
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
