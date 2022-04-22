using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.MainState;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Tackled
{
    public class TackledMainState : BState
    {
        float _waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            _waitTime = 3f;
        }

        public override void Execute()
        {
            base.Execute();

            //decrement time
            _waitTime -= Time.deltaTime;

            //if time if exhausted trigger approprite state transation
            if (_waitTime <= 0)
                SuperMachine.ChangeState<GoToHomeMainState>();
        }
    }
}
