using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class CurveBall : BState
    {
        [Header("Bool")]
        public bool addCurve = false;
        public bool addDip = false;

        [Header("Pass and Shot Power Information")]
        public float curveShootSpeed = 10;
        public float curveShotPower = 5;
        public float curveMin;
        public float curveMax;

        [Header("Animation Bools")]
        private bool isShooting;

        public override void Enter()
        {
            base.Enter();

            //make a shot
            /*
            Owner.MakeShot(Ball.Instance.NormalizedPosition,
                (Vector3)Owner.KickTarget,
                Owner.KickPower,
                Owner.BallTime);
                */

            Ball.Instance.Rigidbody.AddForce(Owner.transform.forward * curveShootSpeed, ForceMode.Impulse);
            Ball.Instance.Rigidbody.AddForce(Owner.transform.up * curveShotPower, ForceMode.Impulse);
            addDip = true;
            addCurve = true;

            //animator.SetBool("isShooting", true);
            //isShooting = true;

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

        IEnumerator CurveAdd()
        {
            Ball.Instance.Rigidbody.AddForce(-Owner.transform.right * Random.Range(curveMin, curveMax) * Time.deltaTime, ForceMode.Impulse);
            yield return new WaitForSeconds(1.5f);
            addCurve = false;
        }
    }
}
