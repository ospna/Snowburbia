using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState;
using Assets.Scripts.Utilities.Enums;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.SubStates
{
    public class ManualControl : BState
    {
        Vector3 RefObjectForward;             // The current forward direction of the camera
        Transform _refObject;                 // A reference to the main camera in the scenes transform

        [Header("Shot Key Code Info")]
        public KeyCode curveShotKeyCode = KeyCode.Z;
        public KeyCode chipShotKeyCode = KeyCode.C;

        public override void Enter()
        {
            base.Enter();

            // enable the user controlled icon
            Owner.IconUserControlled.SetActive(true);

            // set the ref object
            _refObject = Camera.main.transform;

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
            RefObjectForward = Vector3.Scale(_refObject.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 Movement = (input.z * RefObjectForward) + (input.x * _refObject.right);

            // set the direction of movement
            Vector3 direction = Movement == Vector3.zero ? Owner.transform.forward : Movement;

            if (Input.GetButtonDown("Jump"))
            {
                // set the direction of movement
                //Vector3 direction = Movement == Vector3.zero ? Owner.transform.forward : Movement;

                // find pass in direction
                bool canPass = Owner.CanPassInDirection(direction);

                // go to kick ball if can pass
                if(canPass)
                {
                    //go to kick-ball state
                    Owner.KickType = KickType.Pass;
                    SuperMachine.ChangeState<KickBallMainState>();
                }
            }
            else if (Input.GetButtonDown("Shoot") || Input.GetButtonDown("Fire1"))
            {
                // check if I can score
                bool canScore = Owner.CanScore(false, true);

                // shoot if I can score
                if (canScore)
                {
                    //go to kick-ball state
                    Owner.KickType = KickType.Shot;
                    SuperMachine.ChangeState<KickBallMainState>();
                }
                else
                {
                    // reconsider shot without considering the safety
                    canScore = Owner.CanScore(false, false);

                    // shoot if I can score
                    if (canScore)
                    {
                        //go to kick-ball state
                        Owner.KickType = KickType.Shot;
                        SuperMachine.ChangeState<KickBallMainState>();
                    }
                }
            }
            // Curved Shot
            else if(Input.GetKeyDown(curveShotKeyCode))
            {
                Owner.KickType = KickType.CurveShot;
                SuperMachine.ChangeState<KickBallMainState>();
            }
            // Chip Shot
            else if(Input.GetKeyDown(chipShotKeyCode))
            {
                Owner.KickType = KickType.ChipShot;
                SuperMachine.ChangeState<KickBallMainState>();
            }
            else
            {
                //process if any key down
                if (input == Vector3.zero)
                {
                    if (Owner.RPGMovement.Steer == true)
                        Owner.RPGMovement.SetSteeringOff();

                    if (Owner.RPGMovement.Track == true)
                        Owner.RPGMovement.SetTrackingOff();
                }
                else
                {
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
            }
        }

        public override void Exit()
        {
            base.Exit();

            // disable the user controlled icon
            Owner.IconUserControlled.SetActive(false);
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
