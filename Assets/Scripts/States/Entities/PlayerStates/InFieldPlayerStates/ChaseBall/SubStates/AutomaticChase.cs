﻿using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TacklePlayer;
using RobustFSM.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates
{
    public class AutomaticChase : BState
    {
        public Vector3 SteeringTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            //get the steering target
            SteeringTarget = Ball.Instance.NormalizedPosition;

            //set the steering to on
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
            Owner.RPGMovement.SetSteeringOn();
            Owner.RPGMovement.SetTrackingOn();
        }

        public override void Execute()
        {
            base.Execute();

            //check if ball is within control distance
            if (Ball.Instance.Owner != null
                && Owner.IsBallWithinControlableDistance())
            {
                //tackle player
                SuperMachine.ChangeState<TackleMainState>();
            }
            else if (Owner.IsBallWithinControlableDistance())
            {
                // control ball
                SuperMachine.ChangeState<ControlBallMainState>();
            }

            //get the steering target
            SteeringTarget = Ball.Instance.NormalizedPosition;

            //set the steering to on
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
        }

        public override void Exit()
        {
            base.Exit();

            //set the steering to on
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOff();
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
