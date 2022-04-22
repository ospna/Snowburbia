using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class RecoverFromKick : BState
    {
        float waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time 
            waitTime = 0.5f;
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
    }
}
