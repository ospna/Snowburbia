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

            //trigger thr right state transition
            if (Owner.KickType == KickType.Pass)
                Machine.ChangeState<PassBall>();
            else if (Owner.KickType == KickType.Shot)
                Machine.ChangeState<ShootBall>();
            else if (Owner.KickType == KickType.CurveShot)
                Machine.ChangeState<CurveBall>();
            else if (Owner.KickType == KickType.ChipShot)
                Machine.ChangeState<ChipBall>();
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
