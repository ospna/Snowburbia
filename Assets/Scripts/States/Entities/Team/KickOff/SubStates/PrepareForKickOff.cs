using Assets.Scripts.Entities;
//using Assets.Scripts.States.Entities.Team.KickOff.SubStates;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.States.Entities.Team.KickOff.SubStates
{
    public class PrepareForKickOff : BState
    {
        public override void Enter()
        {
            base.Enter();

            SetPlayerCurrentHomePositionToKickOffPosition();

            PlaceEveryPlayerAtKickOffPosition();

            //go to the next state
            if (Owner.HasKickOff)
            {
                PlaceKickOffTakerAtTakeKickOffPosition();
                Machine.ChangeState<TakeKickOff>();
            }
            else
                Machine.ChangeState<WaitForKickOff>();
        }

        void PlaceEveryPlayerAtKickOffPosition()
        {
            Owner.Players.ForEach(tM =>
            {
                tM.Player.Position = tM.CurrentHomePosition.position;
                tM.Player.Rotation = tM.KickOffHomePosition.rotation;
            });
        }

        void PlaceKickOffTakerAtTakeKickOffPosition()
        {
            // get the last player
            TeamPlayer teamPlayer = Owner.Players.Last();

            //get the take kick of state and set the controlling player
            Machine.GetState<TakeKickOff>().ControllingPlayer = teamPlayer;

            //place player a kick off position
            teamPlayer.CurrentHomePosition.position = Pitch.Instance.CenterSpot.position + (Owner.Goal.transform.forward * (teamPlayer.Player.BallControlDistance + teamPlayer.Player.Radius));
            teamPlayer.Player.transform.position = teamPlayer.CurrentHomePosition.position;
            Owner.KickOffRefDirection.position = teamPlayer.Player.transform.position;
            teamPlayer.Player.HomeRegion = Owner.KickOffRefDirection;

            // rotate the player to face the ball
            teamPlayer.Player.transform.rotation = Owner.KickOffRefDirection.rotation;
        }

        void SetPlayerCurrentHomePositionToKickOffPosition()
        {
            Owner.Players.ForEach(tM => tM.CurrentHomePosition.transform.position = tM.KickOffHomePosition.transform.position);
        }

        public Assets.Scripts.Entities.Team Owner
        {
            get
            {
                return ((TeamFSM)SuperMachine).Owner;
            }
        }
    }
}
