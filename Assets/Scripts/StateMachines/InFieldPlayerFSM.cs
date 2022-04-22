using Assets.RobustFSM.Mono;
using Assets.Scripts.Entities;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ChaseBall.ChaseBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ControlBall.ControlBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.GoToHome.GoToHomeMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Init;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.PickOutThreat.PickOutThreatMain;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.ReceiveBall.ReceiveBallMainState;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SupportAttackerMain;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Tackled;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TacklePlayer;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.TakeKickOff;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.Wait;

namespace Assets.Scripts.StateMachines
{
    public class InFieldPlayerFSM : MonoFSM<Player>
    {
        public override void AddStates()
        {
            base.AddStates();

            //set the manual sexecute time
            SetUpdateFrequency(0.5f);

            //add the states
            AddState<ChaseBallMainState>();
            AddState<ControlBallMainState>();
            AddState<GoToHomeMainState>();
            AddState<InitMainState>();
            AddState<KickBallMainState>();
            AddState<PickOutThreatMainState>();
            AddState<ReceiveBallMainState>();
            AddState<SupportAttackerMainState>();
            AddState<TackleMainState>();
            AddState<TackledMainState>();
            AddState<TakeKickOffMainState>();
            AddState<WaitMainState>();

            //set the inital state
            SetInitialState<InitMainState>();
        }
    }
}
