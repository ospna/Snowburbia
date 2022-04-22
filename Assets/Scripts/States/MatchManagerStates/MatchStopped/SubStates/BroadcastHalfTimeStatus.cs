using Assets.Scripts.Managers;
using static Assets.Scripts.Managers.MatchManager;
using Assets.Scripts.StateMachines.Managers;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates
{
    public class BroadcastHalfTimeStatus : BState
    {
        // A reference to the wait time
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            waitTime = 1f;

            //raise the half-start event
            RaiseTheHalfTimeStartEvent();
        }

        public override void Execute()
        {
            base.Execute();

            //decrement the time
            waitTime -= Time.deltaTime;

            //go to wait-for-kick-to-complete if time is less than 0
            if (waitTime <= 0f) Machine.ChangeState<HalfTime>();
        }

        public override void Exit()
        {
            base.Exit();

            //raise the event that first half finished broadcasting
            ActionUtility.Invoke_Action(Owner.OnFinishBroadcastHalfTimeStart);
        }

        // Raises the event to start second half
        public void RaiseTheHalfTimeStartEvent()
        {
            //prepare an empty string
            string message = "Half Time";

            //raise the event
            BroadcastHalfTimeStart temp = Owner.OnBroadcastHalfTimeStart;
            if (temp != null) temp.Invoke(message);
        }

        // Returns the owner of this instance
        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
