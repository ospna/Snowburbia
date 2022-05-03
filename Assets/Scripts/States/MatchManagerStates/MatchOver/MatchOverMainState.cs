using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities;
using RobustFSM.Base;

namespace Assets.Scripts.States.MatchManagerStates.MatchOver
{
    public  class MatchOverMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            //raise the match-end event
            RaiseTheMatchOverEvent();
        }

        public void RaiseTheMatchOverEvent()
        {
            //prepare the message
            string message = string.Empty;

            //generate the message
            if (Owner.TeamAway.IsUserControlled)
            {
                if (Owner.TeamAway.Goals > Owner.TeamHome.Goals)
                    message = "YOU WON! What an incredible performance!";
                else if (Owner.TeamAway.Goals < Owner.TeamHome.Goals)
                    message = "YOU LOST. It was just a bad day at the office.";
                else if (Owner.TeamAway.Goals == 0 && Owner.TeamHome.Goals == 0)
                    message = "And that's that, a bore draw.";
                else
                    message = "A TIE. Both teams were incredible today!";
            }
            else if (Owner.TeamHome.IsUserControlled)
            {
                if (Owner.TeamAway.Goals < Owner.TeamHome.Goals)
                    message = "YOU WON! What an incredible performance!";
                else if (Owner.TeamAway.Goals > Owner.TeamHome.Goals)
                    message = "YOU LOST. It was just a bad day at the office.";
                else if (Owner.TeamAway.Goals == 0 && Owner.TeamHome.Goals == 0)
                    message = "And that's that, a bore draw.";
                else
                    message = "A TIE. Both teams were incredible today!";
            }
            else
            {
                if (Owner.TeamAway.Goals > Owner.TeamHome.Goals)
                    message = "The Away Team Wins!";
                else if (Owner.TeamAway.Goals < Owner.TeamHome.Goals)
                    message = "The Home Team Wins!";
                else
                    message = "Draw";
            }

            //raise the on-match-end-evet
            MatchManager.MatchOver temp = Owner.OnMatchOver;
            if (temp != null) temp.Invoke(message);
        }

        public override void Exit()
        {
            base.Exit();

            //raise the event that I have exited the match over state
            ActionUtility.Invoke_Action(Owner.OnExitMatchOver);

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
