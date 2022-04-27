using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;
using static Assets.Scripts.Managers.MatchManager;

namespace Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates
{
    public class BroadcastHalfStatus : BState
    {
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            waitTime = 1f;

            //raise the half-start event
            RaiseTheHalfStartEvent();
        }

        public override void Execute()
        {
            base.Execute();

            //decrement the time
            waitTime -= Time.deltaTime;

            //go to wait-for-kick-to-complete if time is less than 0
            if (waitTime <= 0f) Machine.ChangeState<WaitForKickOffToComplete>();
        }

        public override void Exit()
        {
            base.Exit();

            //raise the event that I finished broadcasting the start
            //of the first half
            ActionUtility.Invoke_Action(Owner.OnFinishBroadcastHalfStart);
        }

        // Raises the half start event
        public void RaiseTheHalfStartEvent()
        {
            //prepare an empty string
            string message = string.Empty;

            //set the message
            if (Owner.CurrentHalf == 1)
                message = "First Half";
            else
                message = "Second Half";

            //raise the event
            BroadcastHalfStart temp = Owner.OnBroadcastHalfStart;
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
