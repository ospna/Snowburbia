using Assets.Scripts.Managers;
using Assets.Scripts.StateMachines;
using RobustFSM.Base;
using UnityEngine;

namespace Assets.Scripts.States.MatchManagerStates.Init.SubStates
{
    public class Initialize : BState
    {
        int finishedInitializedTeamCount;

        public override void Enter()
        {
            base.Enter();

            //reset the count
            finishedInitializedTeamCount = 0;

            //listen to team OnInit events
            Owner.TeamAway.OnInit += Instance_OnTeamInit;
            Owner.TeamHome.OnInit += Instance_OnTeamInit;


           /*//set some team data
           Owner.TeamAway.Init(Owner.DistancePassMax,
               Owner.DistancePassMin,
               Owner.DistanceShotValidMax,
               Owner.DistanceTendGoal,
               Owner.DistanceThreatMax,
               Owner.DistanceThreatMin,
               Owner.DistanceThreatTrack,
               Owner.DistanceWonderMax,
               Owner.VelocityPassArrive,
               Owner.VelocityShotArrive,
               Owner.Power,
               Owner.Speed);

           Owner.TeamHome.Init(Owner.DistancePassMax,
               Owner.DistancePassMin,
               Owner.DistanceShotValidMax,
               Owner.DistanceTendGoal,
               Owner.DistanceThreatMax,
               Owner.DistanceThreatMin,
               Owner.DistanceThreatTrack,
               Owner.DistanceWonderMax,
               Owner.VelocityPassArrive,
               Owner.VelocityShotArrive,
               Owner.Power,
               Owner.Speed);
               */
        

            //randomize who gets the starting kickoff
            if (Random.value <= 0.5f)
                Owner.TeamAway.HasInitialKickOff = true;
            else
                Owner.TeamHome.HasInitialKickOff = true;

            //set some variables
            Owner.CurrentHalf = 1;
            Owner.NextStopTime = Owner.NormalHalfLength;

            //calculate the in-game 
            TimeManager.Instance.TimeUpdateFrequency = Owner.ActualHalfLength / Owner.NormalHalfLength;

            //enable the teams
            Owner.TeamAway.gameObject.SetActive(true);
            Owner.TeamHome.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            // notify the two teams that the oppossition has finished initializing
            Owner.TeamAway.Invoke_OnOppFinishedInit();
            Owner.TeamHome.Invoke_OnOppFinishedInit();

            //stop listening to team OnInit events
            Owner.TeamAway.OnInit -= Instance_OnTeamInit;
            Owner.TeamHome.OnInit -= Instance_OnTeamInit;
        }

        private void Instance_OnTeamInit()
        {
            ++finishedInitializedTeamCount;

            if (finishedInitializedTeamCount == 2)
            {
                Machine.ChangeState<WaitForMatchOnInstruction>();
            }
        }

        public MatchManager Owner
        {
            get
            {
                return ((MatchManagerFSM)SuperMachine).Owner;
            }
        }
    }
}
