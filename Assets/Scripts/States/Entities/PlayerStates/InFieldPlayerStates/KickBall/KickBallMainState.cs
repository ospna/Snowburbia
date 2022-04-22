﻿using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines.Entities;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.SoccerGameEngine_Basic_.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState
{
    public class KickBallMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add states
            AddState<CheckKickType>();
            AddState<PassBall>();
            AddState<RecoverFromKick>();
            AddState<RotateToFaceTarget>();
            AddState<ShootBall>();

            //set initial state
            SetInitialState<RotateToFaceTarget>();
        }

        public override void Enter()
        {
            base.Enter();

            // set the Ball's rigidbody
            Ball.Instance.Rigidbody.isKinematic = false;
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