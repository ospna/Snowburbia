using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TacklePlayer;
using RobustFSM.Base;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates
{
    public class ManualChase : BState
    {
        bool updateLogic;

        Vector3 RefObjectForward;             // The current forward direction of the camera
        Transform _refObject;                 // A reference to the main camera in the scenes transform
        CinemachineVirtualCamera vCam;

        // The steering target
        public Vector3 SteeringTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            // enable the user controlled icon
            Owner.IconUserControlled.SetActive(true);

            // set the ref object
            _refObject = Camera.main.transform;
            //_refObject = vCam.transform.pos;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);

            // set update logic
            //updateLogic = true;

            //get the steering target
            //SteeringTarget = Ball.Instance.NormalizedPosition;

            //set the steering to on
            //Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            //Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
        }

        public override void Execute()
        {
            base.Execute();

            //capture input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            //calculate the direction to rotate to
            Vector3 input = new Vector3(horizontalInput, 0f, verticalInput);

            // calculate camera relative direction to move:
            //movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
            //movementDirection.Normalize();

            RefObjectForward = Vector3.Scale(_refObject.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 Movement = (input.z * RefObjectForward) + (input.x * _refObject.right);

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);

            //check if ball is within control distance
            if (Ball.Instance.Owner != null && Owner.IsBallWithinControllableDistance())
            {
                //tackle player
                SuperMachine.ChangeState<TackleMainState>();
            }
            else if (Owner.IsBallWithinControllableDistance())
            {
                // control ball
                SuperMachine.ChangeState<ControlBallMainState>();
            }

            if (Input.GetButtonDown("Switch"))
            {
                //Owner.OnBecameTheClosestPlayerToBall;
                ActionUtility.Invoke_Action(Owner.OnBecameTheClosestPlayerToBall);
            }

            // set the movement
            Vector3 moveDirection = Movement == Vector3.zero ? Vector3.zero : Owner.transform.forward;
            Owner.RPGMovement.SetMoveDirection(moveDirection);
            Owner.RPGMovement.SetRotateFaceDirection(Movement);

            // set the steering to on
            if (Owner.RPGMovement.Steer == false)
                Owner.RPGMovement.SetSteeringOn();

            if (Owner.RPGMovement.Track == false)
                Owner.RPGMovement.SetTrackingOn();
        }

        public override void Exit()
        {
            base.Exit();

            // disable the user controlled icon
            Owner.IconUserControlled.SetActive(false);

            //set the steering to on
            Owner.RPGMovement.SetSteeringOff();
            Owner.RPGMovement.SetTrackingOff();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner._animator.SetBool("isJogging", false);
        }

        private void Instance_OnBecameTheClosestPlayerToBall()
        {

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