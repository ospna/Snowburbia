using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState;
using Assets.Scripts.Utilities.Enums;
using RobustFSM.Base;
using Cinemachine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.SubStates
{
    public class ManualControl : BState
    {
        Vector3 RefObjectForward;             // The current forward direction of the camera
        Transform _refObject;                 // A reference to the main camera in the scenes transform
        CinemachineVirtualCamera vCam;

        [Header("Shot Key Code Info")]
        public KeyCode passKeyCode = KeyCode.Space;
        public KeyCode curveShotKeyCode = KeyCode.Z;
        public KeyCode shotKeyCode = KeyCode.Mouse0;
        public KeyCode chipShotKeyCode = KeyCode.C;

        /*
        [Header("Bool")]
        public bool addCurve = false;
        public bool addDip = false;
        public bool playerHasBall = false;

        [Header("Pass and Shot Power Information")]
        public float passSpeed = 500;
        public float curveShootSpeed = 500;
        public float curveShotPower = 350;
        public float shootSpeedForward = 600;
        public float shootSpeedDown = 50;
        public float chipSpeedUp = 400;
        public float chipSpeedForward = 350;
        public float chipTorqueUp = 150;
        public float dribbleSpeed = 100;
        public float curveMin;
        public float curveMax;
        private float forceMagnitude;

        [Header("Game Objects")]
        public GameObject player;
        public Animator animator;

        [Header("References")]
        public GameObject ball;
        public Rigidbody rb;
        public GameObject holdBall;

        [Header("Animation Bools")]
        private bool isPassing;
        private bool isShooting;
        private bool isChipping;
        */

        public override void Enter()
        {
            base.Enter();

            // enable the user controlled icon
            Owner.IconUserControlled.SetActive(true);

            // set the ref object
            _refObject = Camera.main.transform;
            //_refObject = vCam.transform;

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


            if (Input.GetKeyDown(passKeyCode))
            {
                // set the direction of movement
                Vector3 direction = Movement == Vector3.zero ? Owner.transform.forward : Movement;

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
            else if (Input.GetKeyDown(shotKeyCode))
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
            /*
            // Curved Shot
            if (Input.GetKeyDown(curveShotKeyCode))
            {
                rb.AddForce(player.transform.forward * curveShootSpeed, ForceMode.Impulse);
                rb.AddForce(player.transform.up * curveShotPower, ForceMode.Impulse);
                //kickingSound.Play();
                addDip = true;
                addCurve = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isShooting", true);
                isShooting = true;
            }

            // Chip Shot
            if (Input.GetKeyDown(chipShotKeyCode))
            {
                rb.AddForce(player.transform.up * chipSpeedUp, ForceMode.Impulse);
                rb.AddForce(player.transform.forward * chipSpeedForward, ForceMode.Impulse);
                rb.AddTorque(-player.transform.right * chipTorqueUp, ForceMode.Impulse);
                //chipSound.Play();
                addDip = true;
                holdBall.GetComponent<SphereCollider>().enabled = false;

                animator.SetBool("isChipping", true);
                isChipping = true;
            }
            */
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
