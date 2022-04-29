using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class ChipBall : BState
    {
        [Header("Bool")]
        public bool addDip = false;

        [Header("Pass and Shot Power Information")]
        public float chipSpeedUp = 5;
        public float chipSpeedForward = 10;
        public float chipTorqueUp = 2.5f;
        public float curveMin;
        public float curveMax;

        [Header("Animation Bools")]
        private bool isChipping;

        public override void Enter()
        {
            base.Enter();

            /*make a shot
            Owner.MakeShot(Ball.Instance.NormalizedPosition,
                (Vector3)Owner.KickTarget,
                Owner.KickPower,
                Owner.BallTime);
                */

            //Owner.MakeShot(Ball.Instance.NormalizedPosition, (Vector3)Owner.KickTarget, Owner.KickPower, Owner.BallTime);

            Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * chipSpeedUp, ForceMode.Impulse);
            Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * chipSpeedForward, ForceMode.Impulse);
            Ball.Instance.Rigidbody.AddTorque(-Owner.transform.forward * chipTorqueUp, ForceMode.Impulse);
            //chipSound.Play();
            addDip = true;

            // animator.SetBool("isChipping", true);
            //isChipping = true;

            //got to recover state
            Machine.ChangeState<RecoverFromKick>();
        }

        public Player Owner
        {
            get
            {
                return ((InFieldPlayerFSM)SuperMachine).Owner;
            }
        }

        IEnumerator DipAdd()
        {
            Ball.Instance.Rigidbody.AddForce(-Owner.transform.up * 0.1f, ForceMode.Impulse);
            yield return new WaitForSeconds(1.5f);
            addDip = false;
        }
    }
}
