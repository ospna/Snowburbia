using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class PassBall : BState
    {
        public override void Enter()
        {
            base.Enter();

            // set the prev pass receiver
            Owner.PrevPassReceiver = Owner.PassReceiver;

            //make a normal pass to the player
            Owner.MakePass(Ball.Instance.NormalizedPosition, (Vector3)Owner.KickTarget, Owner.PassReceiver,Owner.KickPower, Owner.BallTime);

            Owner.GetComponentInChildren<Animator>().SetBool("isPassing", true);
            Owner.gingAnim.SetBool("isPassing", true);

            //go to recover state
            Machine.ChangeState<RecoverFromKick>();
        }

        public override void Exit()
        {
            base.Exit();

            // reset the ball owner
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
