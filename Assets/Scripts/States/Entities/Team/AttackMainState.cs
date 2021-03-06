using System;
using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.Team.Defend;
using Assets.Scripts.States.Entities.Team.Wait;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.Entities.Team.Attack
{
    public class AttackMainState : BState
    {
        float _lengthPitch = 60;

        public override void Enter()
        {
            base.Enter();

            // enable the support spots root
            Owner.PlayerSupportSpots.gameObject.SetActive(true);

            //listen to some team events
            Owner.OnLostPossession += Instance_OnLoosePossession;
            Owner.OnMessagedToStop += Instance_OnMessagedToStop;

            // init the players home positions
            Owner.Players.ForEach(tM => ActionUtility.Invoke_Action(tM.Player.OnInstructedToGoToHome));
        }

        public override void ManualExecute()
        {
            base.ManualExecute();

            //loop through each player and update it's position
            foreach(TeamPlayer teamPlayer in Owner.Players)
            {
                //find the percentage to move the player upfield
                Vector3 ballGoalLocalPosition = Owner.Goal.transform.InverseTransformPoint(Ball.Instance.transform.position);
                float playerMovePercentage = Mathf.Clamp01((ballGoalLocalPosition.z / _lengthPitch) + 0.5f);

                //move the home position a similar percentage up the field
                Vector3 currentPlayerHomePosition = Vector3.Lerp(teamPlayer.DefendingHomePosition.transform.position,
                    teamPlayer.AttackingHomePosition.position, playerMovePercentage);

                //update the current player home position position
                if(Vector3.Distance(currentPlayerHomePosition, teamPlayer.CurrentHomePosition.position) >= 1)
                    teamPlayer.CurrentHomePosition.position = currentPlayerHomePosition;
            }
        }

        public override void Exit()
        {
            base.Exit();

            // enable the support spots root
            Owner.PlayerSupportSpots.gameObject.SetActive(false);

            //stop listening to some team events
            Owner.OnLostPossession -= Instance_OnLoosePossession;
            Owner.OnMessagedToStop -= Instance_OnMessagedToStop;
        }

        private void Instance_OnLoosePossession()
        {
            Machine.ChangeState<DefendMainState>();
        }

        private void Instance_OnMessagedToStop()
        {
            SuperMachine.ChangeState<WaitMainState>();
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
