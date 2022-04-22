using Assets.RobustFSM.Mono;
using Assets.Scripts.Managers;
using Assets.Scripts.States.MatchManagerStates.Init;
using Assets.Scripts.States.MatchManagerStates.MatchOn;
using Assets.Scripts.States.MatchManagerStates.MatchOver;
using RobustFSM.Base;

namespace Assets.Scripts.StateMachines
{
    public class MatchManagerFSM : MonoFSM<MatchManager>
    {
        public override void AddStates()
        {
            //add the states
            AddState<InitMainState>();
            AddState<MatchOnMainState>();
            AddState<MatchOverMainState>();

            //set the initial state
            SetInitialState<InitMainState>();
        }
    }
}
