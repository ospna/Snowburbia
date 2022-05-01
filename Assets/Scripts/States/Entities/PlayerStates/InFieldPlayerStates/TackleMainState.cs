using Assets.Scripts.Entities;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TacklePlayer
{
    public class TackleMainState : BState
    {
        bool _isTackleSuccessful;
        float _waitTime;

        public override void Enter()
        {
            base.Enter();

            //set the wait time
            _waitTime = 0.15f;

            //randomly find who will win this 50/50
            _isTackleSuccessful = Random.value <= 0.5f;

            //if tackle is successful, then message the ball owner
            //that he has been tackled
            if(_isTackleSuccessful)
                ActionUtility.Invoke_Action(Ball.Instance.Owner.OnTackled);
        }

        public override void Execute()
        {
            base.Execute();

            //decrement time
            _waitTime -= Time.deltaTime;

            //if time if exhausted trigger approprite state transation
            if(_waitTime <= 0)
            {
                if (_isTackleSuccessful)
                    SuperMachine.ChangeState<ControlBallMainState>();
                else
                    SuperMachine.ChangeState<GoToHomeMainState>();
            }
        }
    }
}
