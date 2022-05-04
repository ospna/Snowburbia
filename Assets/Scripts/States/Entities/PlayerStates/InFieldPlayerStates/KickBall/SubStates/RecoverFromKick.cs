using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
using RobustFSM.Base;
using UnityEngine;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities.Enums;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class RecoverFromKick : BState
    {
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time 
            waitTime = 0.25f;

            Owner.GetComponentInChildren<Animator>().SetBool("isPassing", false);
            Owner._animator.SetBool("isPassing", false);

            Owner.GetComponentInChildren<Animator>().SetBool("isShooting", false);
            Owner._animator.SetBool("isShooting", false);

        }

        public override void Execute()
        {
            base.Execute();

            //decrement time
            waitTime -= Time.deltaTime;

            //go to home after state
            if (waitTime <= 0f)
                SuperMachine.ChangeState<GoToHomeMainState>();
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
