using Assets.Scripts.States.Entities.Team.KickOff.SubStates;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.Team.KickOff.KickOffMainState
{
    public class KickOffMainState : BHState
    {
        public override void AddStates()
        {
            //add the states
            AddState<TakeKickOff>();
            AddState<PrepareForKickOff>();
            AddState<WaitForKickOff>();

            //set the initial state
            SetInitialState<PrepareForKickOff>();
        }
    }
}
