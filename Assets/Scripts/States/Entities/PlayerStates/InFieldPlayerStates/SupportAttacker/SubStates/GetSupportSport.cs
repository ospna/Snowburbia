using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SupportAttackerMain;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.SupportAttacker.SubStates
{
    public class GetSupportSport : BState
    {
        public override void Enter()
        {
            base.Enter();

            SupportSpot supportSpot = ((SupportAttackerMainState)Machine).SupportSpot;

            if (supportSpot == null)
                Machine.ChangeState<SteerToHome>();
            else
                Machine.ChangeState<SteerToSupportSpot>();
        }

        public Player Owner
        {
            get
            {
                return ((InFieldPlayerFSM)SuperMachine).Owner;
            }
        }
    }
}
