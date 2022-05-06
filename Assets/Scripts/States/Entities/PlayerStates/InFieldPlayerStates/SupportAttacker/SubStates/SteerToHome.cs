using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SupportAttackerMain;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SubStates
{
    public class SteerToHome : BState
    {
        int waitTime;
        SupportSpot _supportSpot;

        Vector3? ParentSteeringTarget { get; set; }

        // The steering target
        public Vector3 SteeringTarget { get; set; }

        public override void Enter()
        {
            base.Enter();

            //init wait time
            waitTime = 1;

            //get the steering target
            SteeringTarget = Owner.HomeRegion.position;

            //set the steering to on
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
            Owner.RPGMovement.SetSteeringOn();
            Owner.RPGMovement.SetTrackingOn();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner.snowAnim.SetBool("isJogging", true);
            Owner.gingAnim.SetBool("isJogging", true);
        }

        public override void Execute()
        {
            base.Execute();

            //check if now at target and switch to wait for ball
            if (Owner.IsAtTarget(SteeringTarget))
                Machine.ChangeState<WaitAtTarget>();

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", false);
            Owner.snowAnim.SetBool("isJogging", false);
            Owner.gingAnim.SetBool("isJogging", false);
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            //decrement wait time
            waitTime -= 1;

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner.snowAnim.SetBool("isJogging", true);
            Owner.gingAnim.SetBool("isJogging", true);

            //if I waited enough then consider going home
            if (waitTime <= 0)
            {
                //init wait time
                waitTime = 1;

                // get the support spot
                _supportSpot = ((SupportAttackerMainState)Machine).SupportSpot;

                // if there is a steering target then go to move to support spot
                if (_supportSpot != null)
                    Machine.ChangeState<SteerToSupportSpot>();
            }

            //update the steering target
            SteeringTarget = Owner.HomeRegion.position;

            //update the rpg movement
            Owner.RPGMovement.SetMoveTarget(SteeringTarget);
            Owner.RPGMovement.SetRotateFacePosition(SteeringTarget);
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
