using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.SubStates;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.InFieldPlayerStates.KickBall.KickBallMainState
{
    public class KickBallMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add states
            AddState<CheckKickType>();
            AddState<PassBall>();
            AddState<RecoverFromKick>();
            AddState<RotateToFaceTarget>();
            AddState<ShootBall>();
            AddState<CurveBall>();
            AddState<ChipBall>();

            //set initial state
            SetInitialState<RotateToFaceTarget>();
        }

        public override void Enter()
        {
            base.Enter();

            // set the Ball's rigidbody
            Ball.Instance.Rigidbody.isKinematic = false;
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
