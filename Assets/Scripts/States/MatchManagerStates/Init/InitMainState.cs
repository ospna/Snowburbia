using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MatchManagerStates.Init.SubStates;
using RobustFSM.Base;

namespace Assets.Scripts.States.MatchManagerStates.Init
{
    public class InitMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add states
            AddState<Initialize>();
            AddState<WaitForMatchOnInstruction>();

            //set initial state
            SetInitialState<Initialize>();
        }

        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
