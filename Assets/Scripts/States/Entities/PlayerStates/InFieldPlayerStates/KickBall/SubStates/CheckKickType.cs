using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.Utilities.Enums;
using RobustFSM.Base;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates
{
    public class CheckKickType : BState
    {
        public override void Enter()
        {
            base.Enter();

            //trigger the right state transition
            if (Owner.KickType == KickType.Pass)
                Machine.ChangeState<PassBall>();

            if (Owner.KickType == KickType.Shot)
                Machine.ChangeState<ShootBall>();

            /*
            if (Owner.KickType == KickType.CurveShot)
                Machine.ChangeState<CurveBall>();

            if (Owner.KickType == KickType.ChipShot)
                Machine.ChangeState<ChipBall>();
                */
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
