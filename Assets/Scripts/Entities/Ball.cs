using System;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.SubStates;
using Patterns.Singleton;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Ball : Singleton<Ball>
    {
        [SerializeField]
        float _friction = 3f;

        [SerializeField]
        float _gravity = 9.11f;

        [SerializeField]
        string _groundMaskName;

        bool _isGrounded;
        float _rayCastDistance;
        int _groundMask;
        RaycastHit _hit;
        Vector3 _frictionVector;
        Vector3 _rayCastStartPosition;

        public delegate void BallLaunched(float flightTime, float velocity, Vector3 initial, Vector3 target);

        public BallLaunched OnBallLaunched;
        public BallLaunched OnBallShot;

        public float Friction { get => -_friction; set => _friction = value; }

        public Player Owner { get; set; }

        public Team _team { get; set; }

        public Rigidbody Rigidbody { get; set; }
        public SphereCollider SphereCollider { get; set; }

        public override void Awake()
        {
            base.Awake();
            Rigidbody = GetComponent<Rigidbody>();
            SphereCollider = GetComponent<SphereCollider>();

            _groundMask = LayerMask.GetMask(_groundMaskName);
            _rayCastDistance = SphereCollider.radius + 0.05f;
        }

        private void FixedUpdate()
        {
            ApplyFriction();

            /*
            if(Owner == null)
            {
                //Owner.InFieldPlayerFSM.SetCurrentState<AutomaticChase>();
                //Owner.OnChaseBall(Owner);
                _team.Invoke_OnPlayerChaseBall(Owner);
            }
            */
        }

        // Applies friction to this instance
        public void ApplyFriction()
        {
            //get the direction the ball is travelling
            _frictionVector = Rigidbody.velocity.normalized;
            _frictionVector.y = 0f;

            //calculate the actual friction
            _frictionVector *= -1 * _friction;

            //calculate the raycast start positiotn
            _rayCastStartPosition = transform.position + SphereCollider.radius * Vector3.up;

            //check if the ball is touching with the pitch
            _isGrounded = Physics.Raycast(_rayCastStartPosition, Vector3.down, out _hit, _rayCastDistance, _groundMask);

            //apply friction if grounded
            if (_isGrounded)
            {
                Rigidbody.AddForce(_frictionVector);
            }
        }

        // Finds the power needed to kick the ball to reach it's destination
        public float FindPower(Vector3 from, Vector3 to, float finalVelocity)
        {
            return Mathf.Sqrt(Mathf.Pow(finalVelocity, 2f) - (2 * -_friction * Vector3.Distance(from, to)));
        }

        // Kicks the ball to the target
        public void Kick(Vector3 to, float power)
        {
            Vector3 direction = to - NormalizedPosition;
            direction.Normalize();

            //change the velocity
            Rigidbody.velocity = direction * power;

            //invoke the ball launched event
            BallLaunched temp = OnBallLaunched;
            if (temp != null)
            {
                temp.Invoke(0f, power, NormalizedPosition, to);
            }
        }

        public void Launch(float power, Vector3 final)
        {
            //set the initial position
            Vector3 initial = Position;

            //find the direction vectors
            Vector3 toTarget = final - initial;
            Vector3 toTargetXZ = toTarget;
            toTargetXZ.y = 0;

            //find the time to target
            float time = toTargetXZ.magnitude / power;

            // calculate starting speeds for xz and y
            // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
            toTargetXZ = toTargetXZ.normalized * toTargetXZ.magnitude / time;

            //set the y-velocity
            Vector3 velocity = toTargetXZ;
            velocity.y = toTarget.y / time + (0.5f * _gravity * time);

            //return the velocity
            Rigidbody.velocity = velocity;

            //invoke the ball launched event
            BallLaunched temp = OnBallLaunched;
            if (temp != null)
            {
                temp.Invoke(time, power, initial, final);
            }
        }

        public void Trap()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
        }

        public float TimeToCoverDistance(Vector3 from, Vector3 to, float initialVelocity, bool factorInFriction = true)
        {
            //find the distance
            float distance = Vector3.Distance(from, to);

            //simply assume there is no friction(ball is self accelerating)
            if(!factorInFriction || (factorInFriction && _friction == 0))
            {
                return distance / initialVelocity;
            }
            else
            {
                float v_squared = Mathf.Pow(initialVelocity, 2f) + (2 * _friction * Vector3.Distance(from, to));

                if (v_squared <= 0)
                {
                    return -1.0f;
                }
    
                return (Mathf.Sqrt(v_squared) - initialVelocity) / (_friction);
            }
        }

        // Get the normalized ball position
        public Vector3 NormalizedPosition
        {
            get
            {
                return new Vector3(transform.position.x, 0, transform.position.z);
            }

            set
            {
                transform.position = new Vector3(value.x, 0.25f, value.z);
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }

            set
            {
                transform.position = value;
            }
        }
    }
}
