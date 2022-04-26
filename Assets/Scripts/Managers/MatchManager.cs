using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.Team.Attack;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Enums;
using Patterns.Singleton;
using RobustFSM.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    [RequireComponent(typeof(MatchManagerFSM))]
    public class MatchManager : Singleton<MatchManager>
    {
        [SerializeField]
        float _distancePassMax = 15f;

        [SerializeField]
        float _distancePassMin = 5f;

        [SerializeField]
        float _distanceShotValidMax = 15;

        [SerializeField]
        float _distanceTendGoal = 3f;

        [SerializeField]
        float _distanceThreatMax = 1f;

        [SerializeField]
        float _distanceThreatMin = 5f;

        [SerializeField]
        float _distanceThreatTrack = 1f;

        [SerializeField]
        float _distanceWonderMax = 15f;

        [SerializeField]
        float _velocityPassArrive = 5f;

        [SerializeField]
        float _velocityShotArrive = 10f;

        [SerializeField]
        float _power = 10f;

        [SerializeField]
        float _speed = 2.5f;

        [SerializeField]
        Team _teamAway;

        [SerializeField]
        Team _teamHome;

        [SerializeField]
        Transform _rootTeam;

        [SerializeField]
        Transform _transformCentreSpot;
                
        // A reference to how long each half length is in actual time(m)
        public float ActualHalfLength { get; set; } = 2f;
                
        // A reference to the normal half length
        public float NormalHalfLength { get; set; } = 45;
                
        // A reference to the next time that we have to stop the game
        public float NextStopTime { get; set; }

        // A reference to the current game half in play
        public int CurrentHalf { get; set; }

        // Property to get or set this instance's fsm
        public IFSM FSM { get; set; }

        // A reference to the match status of this instance
        public MatchStatuses MatchStatus { get; set; }

        // Event raised when this instance is instructed to go to the second half
        public Action OnContinueToSecondHalf;

        // Event raised when we enter the wait for kick-off state 
        public Action OnEnterWaitForKickToComplete;

        // Event raised when we enter the wait for match on state
        public Action OnEnterWaitForMatchOnInstruction;

        // Event raised when we exist the halftime
        public Action OnExitHalfTime;

        // Event raised when this instance exists the match over state
        public Action OnExitMatchOver { get; set; }

        // Event raised when we exits the wait for kick-off state 
        public Action OnExitWaitForKickToComplete;

        // Event raised when we exist the wait for match on state
        public Action OnExitWaitForMatchOnInstruction;

        // Event raised when this instance finishes broadcasting an event
        public Action OnFinishBroadcastHalfStart;

        // Event raised when this instance finishes broadcasting the half-time-start event
        public Action OnFinishBroadcastHalfTimeStart;

        // Event raised when this instance finishes broadcasting the match start event
        public Action OnFinishBroadcastMatchStart;
        
        // Raised when match play starts
        public Action OnMatchPlayStart;

        // Raised when match play stops
        public Action OnMatchPlayStop;

        // Action to be raised when the match is stopped
        public Action OnStopMatch;

        // Event raised to instruct this instance to switch to match on state
        public Action OnMesssagedToSwitchToMatchOn;
  
        // Action to be raised when the kick-off needs to be taken
        public Action OnBroadcastTakeKickOff;

        // Goal scored delegate
        public delegate void GoalScored(string message, string message1);

        // Match end delegate
        public delegate void BroadcastHalfStart(string message);

        // </summary>
        public delegate void BroadcastHalfTimeStart(string message);

        // Half time start delegate
        public delegate void EnterHalfTime(string message);

        // Match start delegate
        public delegate void BroadcastMatchStart(string message);

        // Match end delegate
        public delegate void MatchOver(string message);

        // Tick delegate
        public delegate void Tick(int half, int minutes, int seconds);

        // Event that is raised when a goal is scored
        public GoalScored OnGoalScored;

        // Event raised when the half starts
        public BroadcastHalfStart OnBroadcastHalfStart;

        // Event raised when the half time starts
        public BroadcastHalfTimeStart OnBroadcastHalfTimeStart;

        // Event raised when we enter half time
        public EnterHalfTime OnEnterHalfTime;

        // Event to be raised when a match ends
        public MatchOver OnMatchOver;

        // Event to be raised when a broadcast of match starts is started
        public BroadcastMatchStart OnBroadcastMatchStart;

        // The OnTick event
        public Tick OnTick;

        public override void Awake()
        {
            base.Awake();

            FSM = GetComponent<MatchManagerFSM>();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(TeamAway.FSM.IsCurrentState<AttackMainState>())
                {
                    ActionUtility.Invoke_Action(TeamHome.OnGainPossession);
                }
                else if (TeamHome.FSM.IsCurrentState<AttackMainState>())
                {
                    ActionUtility.Invoke_Action(TeamAway.OnGainPossession);
                }
            }
        }
        public void Instance_OnContinueToSecondHalf()
        {
            ActionUtility.Invoke_Action(OnContinueToSecondHalf);
        }

        // Raises the event that this instance has been messaged to switch to match on
        public void Instance_OnMessagedSwitchToMatchOn()
        {
            ActionUtility.Invoke_Action(OnMesssagedToSwitchToMatchOn);
        }

        public float DistanceThreatMin
        {
            get => _distanceThreatMin;
            set => _distanceThreatMin = value;
        }

        public float DistanceThreatMax
        {
            get => _distanceThreatMax;
            set => _distanceThreatMax = value;
        }

        public float Power { get => _power; }
        public float Speed { get => _speed; }

        public Team TeamAway { get => _teamAway; }
        public Team TeamHome { get => _teamHome; }

        // Property to access the team root transform
        public Transform RootTeam { get => _rootTeam; }
        public float DistancePassMax { get => _distancePassMax; set => _distancePassMax = value; }
        public float DistancePassMin { get => _distancePassMin; set => _distancePassMin = value; }
        public Transform TransformCentreSpot { get => _transformCentreSpot; set => _transformCentreSpot = value; }
        public float DistanceWonderMax { get => _distanceWonderMax; set => _distanceWonderMax = value; }
        public float DistanceShotValidMax { get => _distanceShotValidMax; set => _distanceShotValidMax = value; }
        public float VelocityPassArrive { get => _velocityPassArrive; set => _velocityPassArrive = value; }
        public float VelocityShotArrive { get => _velocityShotArrive; set => _velocityShotArrive = value; }
        public float DistanceTendGoal { get => _distanceTendGoal; set => _distanceTendGoal = value; }
        public float DistanceThreatTrack { get => _distanceThreatTrack; set => _distanceThreatTrack = value; }
    }
}
