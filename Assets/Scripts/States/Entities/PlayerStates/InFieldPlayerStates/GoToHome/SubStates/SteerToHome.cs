using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.SubStates
{
    public class SteerToHome : BState
    {
        // The steering target
        public Vector3 SteeringTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            //get the steering target
            SteeringTarget = Owner.HomeRegion.position;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);

            //set the steering to on
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
            Owner.RPGMovement.SetSteeringOn();
            Owner.RPGMovement.SetTrackingOn();
        }

        public override void Execute()
        {
            base.Execute();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);

            //check if now at target and switch to wait for ball
            if (Owner.IsAtTarget(SteeringTarget))
                Machine.ChangeState<WaitAtHome>();
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner._animator.SetBool("isJogging", true);

            //update the steering target
            SteeringTarget = Owner.HomeRegion.position;

            //update the rpg movement
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
        }

        public override void Exit()
        {
            base.Exit();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner._animator.SetBool("isJogging", false);
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
