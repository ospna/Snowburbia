using Assets.RobustFSM.Mono;
using Assets.Scripts.Entities;
using Assets.Scripts.States.Entities.Team.Attack;
using Assets.Scripts.States.Entities.Team.Defend;
using Assets.Scripts.States.Entities.Team.Init;
using Assets.Scripts.States.Entities.Team.KickOff.KickOffMainState;
using Assets.Scripts.States.Entities.Team.Wait;

namespace Assets.Scripts.StateMachines
{
    public class TeamFSM : MonoFSM<Team>
    {
        public override void AddStates()
        {
            //set the update frequency
            SetUpdateFrequency(.25f);

            //add the states
            AddState<AttackMainState>();
            AddState<InitMainState>();
            AddState<DefendMainState>();
            AddState<KickOffMainState>();
            AddState<WaitMainState>();

            //set the initial state
            SetInitialState<InitMainState>();
        }
    }
}
