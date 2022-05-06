using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.SubStates;
using Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.ProtectGoal;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.PlayerStates.GoalKeeperStates.GoToHome.GoToHomeMainState
{
    public class GoToHomeMainState : BHState
    {
        public override void AddStates()
        {
            base.AddStates();

            //add the states
            AddState<SteerToHome>();
            AddState<WaitAtHome>();

            //set the initial state
            SetInitialState<SteerToHome>();
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            // run logic depending on whether team is in control or not
            if (Owner.IsTeamInControl == false)
                SuperMachine.ChangeState<ProtectGoalMainState>();

            if (Owner.IsBallWithinControllableDistance())
            {
                // find direction to deflect ball to
                Vector3 localPoint = Owner.TeamGoal.transform.InverseTransformPoint(Owner.Position);
                localPoint.y = localPoint.z = 0f;

                // find the direction in world space
                Vector3 direction = Owner.TeamGoal.transform.TransformPoint(localPoint);

                // deflect ball
                Ball.Instance.Kick(Owner.Position + direction.normalized, Ball.Instance.Rigidbody.velocity.magnitude * -1.5f);
            }

            Owner.GetComponentInChildren<Animator>().SetBool("isJogging", true);
            Owner.snowAnim.SetBool("isJogging", true);
            Owner.gingAnim.SetBool("isJogging", true);
        }

        public Player Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }
    }
}
