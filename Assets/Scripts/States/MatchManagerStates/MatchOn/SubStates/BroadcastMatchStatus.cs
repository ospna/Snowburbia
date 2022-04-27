using Assets.Scripts.Managers;
using static Assets.Scripts.Managers.MatchManager;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates
{
    // Broadcasts the match status just before the match status is executed
    public class BroadcastMatchStatus : BState
    {
        // A reference to the wait time
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            waitTime = .5f;

            //raise the half-start event
            RaiseTheMatchStartEvent();
        }

        public override void Execute()
        {
            base.Execute();

            //decrement the time
            waitTime -= Time.deltaTime;

            //go to wait-for-kick-to-complete if time is less than 0
            if (waitTime <= 0f) Machine.ChangeState<BroadcastHalfStatus>();
        }

        public override void Exit()
        {
            base.Exit();

            ActionUtility.Invoke_Action(Owner.OnFinishBroadcastMatchStart);
        }

        // Raises the start event
        public void RaiseTheMatchStartEvent()
        {
            //prepare an empty string
            string message = "";

            //raise the event
            BroadcastMatchStart temp = Owner.OnBroadcastMatchStart;
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
