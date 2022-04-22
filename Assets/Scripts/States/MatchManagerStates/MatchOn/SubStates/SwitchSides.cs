using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using RobustFSM.Interfaces;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.MatchOn.SubStates
{
    public class SwitchSides : BState
    {
        public override void Enter()
        {
            base.Enter();

            //set-up the game scene
            Owner.CurrentHalf = 2;
            Owner.NextStopTime += Owner.NormalHalfLength;
            Owner.RootTeam.transform.Rotate(Owner.RootTeam.transform.rotation.eulerAngles + new Vector3(0f, 180f, 0f));

            // set the team's kickoff
            Owner.TeamAway.HasKickOff = !Owner.TeamAway.HasInitialKickOff;
            Owner.TeamHome.HasKickOff = !Owner.TeamHome.HasInitialKickOff;

            //got back to wait for kick-off state
            ((IState)Machine).Machine.ChangeState<BroadcastHalfStatus>();
        }

        // Access the super state machine
        public IFSM SuperFSM
        {
            get
            {
                return (MatchManagerFSM)SuperMachine;
            }
        }

        // Access the owner of the state machine
        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
