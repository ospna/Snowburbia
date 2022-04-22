using Assets.Scripts.Managers;
using static Assets.Scripts.Managers.MatchManager;
using Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using RobustFSM.Interfaces;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.MatchStopped.SubStates
{
    public class BroadcastGoalScored : BState
    {
        // A reference to the wait time
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            waitTime = 3f;

            //raise the half-start event
            RaiseTheGoalScoredEvent();
        }

        public override void Execute()
        {
            base.Execute();

            //decrement the time
            waitTime -= Time.deltaTime;

            //go to wait-for-kick-to-complete if time is less than 0
            if (waitTime <= 0f)
            {
                ((IState)Machine).Machine.ChangeState<WaitForKickOffToComplete>();
            }
        }

        public override void Exit()
        {
            base.Exit();

            //raise the event that first half finished broadcasting
            ActionUtility.Invoke_Action(Owner.OnFinishBroadcastHalfStart);
        }

        // Raises the half start event
        public void RaiseTheGoalScoredEvent()
        {
            //prepare an empty string
            string message = "Goal";

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
