using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates;
using Assets.Scripts.Utilities.Enums;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.SubStates
{
    public class ManualControl : BState
    {
        Vector3 RefObjectForward;             // The current forward direction of the camera
        Transform _refObject;                 // A reference to the main camera in the scenes transform

        [Header("Pass and Shot Power Information")]
        public float passSpeed = 50;
        public float curveShootSpeed = 50;
        public float curveShotPower = 35;
        public float shootSpeedForward = 60;
        public float shootSpeedDown = 5;
        public float chipSpeedUp = 40;
        public float chipSpeedForward = 35;
        public float chipTorqueUp = 15;
        public float dribbleSpeed = 10;
        public float curveMin;
        public float curveMax;
        private float forceMagnitude;

        [Header("Game Objects")]
        private GameObject player;
        private Animator animator;

        [Header("Shot Key Code Info")]
        public KeyCode passKeyCode = KeyCode.Mouse1;
        public KeyCode curveShotKeyCode = KeyCode.Z;
        public KeyCode shootKeyCode = KeyCode.Mouse0;
        public KeyCode chipShotKeyCode = KeyCode.C;

        [Header("References")]
        public GameObject ball;
        public Rigidbody rb;
        public GameObject holdBall;

        [Header("Audio")]
        public AudioSource kickingSound;
        public AudioSource chipSound;
        private AudioSource bounceSound;
        private AudioSource dribbleSound;

        [Header("Bool")]
        public bool addCurve = false;
        public bool addDip = false;
        public bool playerHasBall = false;

        [Header("Animation Bools")]
        private bool isPassing;
        private bool isShooting;
        private bool isChipping;

        public Ball.BallLaunched OnBallLaunched;
        public Ball.BallLaunched OnBallShot;

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

                // bool canPass = Owner.CanPassInDirection(direction);
                bool canPass = Owner.CanPass(true);

                if (canPass)
                {
                    //go to kick-ball state
                    Owner.KickType = KickType.Pass;
                    SuperMachine.ChangeState<KickBallMainState>();
                }
            }
            if (Input.GetButtonDown("Shoot") || Input.GetButtonDown("Fire1"))
            {
                // check if I can score
                bool canScore = Owner.CanScore(true, true);


                if(canScore)
                {
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
            if (Input.GetButtonDown("Jump"))
            {

                Ball.Instance.Rigidbody.AddRelativeForce(Owner.transform.forward * passSpeed, ForceMode.Impulse);

                Ball.Instance.Launch(passSpeed, Owner.transform.forward);

                Ball.Instance.Owner = null;

                /*
                // pass the ball if it's in front of me else go back to control ball
                if (Owner.IsInfrontOfPlayer((Vector3)Owner.KickTarget)
                {
                    if (Owner.KickType == KickType.Pass)
                    {
                        Machine.ChangeState<CheckKickType>();
                    }
                    else
                        Machine.ChangeState<CheckKickType>();
                }


                //Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * passSpeed, ForceMode.Impulse);
                //Owner.GetComponent<Rigidbody>().AddForce(Ball.Instance.transform.forward * passSpeed, ForceMode.Impulse);

                //make a normal pass to the player
                //Owner.MakePass(Ball.Instance.NormalizedPosition, Owner.transform.forward, null, passSpeed, Owner.BallTime);

                //Ball.Instance.Kick(Vector3.forward, passSpeed);


                //Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * passSpeed, ForceMode.Impulse);

                //addDip = true;

                // set the direction of movement
                //Vector3 direction = Movement == Vector3.zero ? Owner.transform.forward : Movement;

                /*
                // find pass in direction
                bool canPass = Owner.CanPassInDirection(direction);

                // go to kick ball if can pass
                if(canPass)
                {
                    //go to kick-ball state
                    Owner.KickType = KickType.Pass;
                    SuperMachine.ChangeState<KickBallMainState>();
                }
                /
            }
            if (Input.GetButtonDown("Shoot") || Input.GetButtonDown("Fire1"))
            {
                //Ball.Instance.Rigidbody.AddForce(-Owner.transform.up * shootSpeedDown, ForceMode.Impulse);
                //Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * shootSpeedForward, ForceMode.Impulse);

                // check if I can score
                bool canScore = Owner.CanScore(true, true);


                if (canScore)
                {
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
            else if(Input.GetKeyDown(curveShotKeyCode))
            {
                /*
                Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * curveShootSpeed, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddForce(Owner.transform.up * curveShotPower, ForceMode.Impulse);
                addDip = true;
                addCurve = true;
                /

                Owner.KickType = KickType.CurveShot;
                SuperMachine.ChangeState<CurveBall>();
            }
            // Chip Shot
            else if(Input.GetKeyDown(chipShotKeyCode))
            {
                Owner.KickType = KickType.ChipShot;
                SuperMachine.ChangeState<ChipBall>();
                /*
                Ball.Instance.Rigidbody.AddForceAtPosition(Owner.transform.up * chipSpeedUp, (Vector3)Owner.KickTarget, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * chipSpeedForward, ForceMode.Impulse);
                Ball.Instance.Rigidbody.AddTorque(-Owner.transform.right * chipTorqueUp, ForceMode.Impulse);
                addDip = true;
                /
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

        /*
        // Kicks the ball
        public void Kick(Vector3 to, float power)
        {
            Vector3 direction = to - Ball.Instance.NormalizedPosition;
            direction.Normalize();

            //change the velocity
            Ball.Instance.Rigidbody.velocity = direction * power;

            //invoke the ball launched event
            Ball.BallLaunched temp = OnBallLaunched;
            if (temp != null)
            {
                temp.Invoke(0f, power, Ball.Instance.NormalizedPosition, to);
            }
        }
        */

        public override void Exit()
        {
            base.Exit();

            // disable the user controlled icon
            Owner.IconUserControlled.SetActive(false);

            Ball.Instance.Owner = null;
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
