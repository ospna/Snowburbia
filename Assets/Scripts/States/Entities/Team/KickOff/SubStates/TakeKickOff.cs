using Assets.Scripts.Entities;
using Assets.Scripts.StateMachines;
using Assets.Scripts.States.Entities.Team.Attack;
using Assets.Scripts.Utilities;
using RobustFSM.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.States.Entities.Team.KickOff.SubStates
{
    public class TakeKickOff : BState
    {
        bool executed;
        float waitTime = 1f;

        Action InstructPlayerToTakeKickOff;

        public TeamPlayer ControllingPlayer { get; set; }

        public override void Enter()
        {
            base.Enter();

            // set to unexecuted
            executed = false;

            // register player to listening to take-kickoff action
            ControllingPlayer.Player.OnTakeKickOff += Instance_OnPlayerTakeKickOff;
            InstructPlayerToTakeKickOff += ControllingPlayer.Player.Invoke_OnInstructedToTakeKickOff;

        }

        public override void Execute()
        {
            base.Execute();

            if (!executed)
            {
                // decrement time
                waitTime -= Time.deltaTime;

                if (waitTime <= 0)
                {
                    // set to executed
                    executed = true;

                    // trigger player to take kick-off
                    ActionUtility.Invoke_Action(InstructPlayerToTakeKickOff);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();

            // deregister player from listening to take-kickoff action
            ControllingPlayer.Player.OnTakeKickOff -= Instance_OnPlayerTakeKickOff;
            InstructPlayerToTakeKickOff -= ControllingPlayer.Player.Invoke_OnInstructedToTakeKickOff;

            // reset the home region of the player
            ControllingPlayer.Player.HomeRegion = ControllingPlayer.CurrentHomePosition;
        }

        public void Instance_OnPlayerTakeKickOff()
        {
            // trigger state change to attack
            SuperMachine.ChangeState<AttackMainState>();

            //simply raise that I have taken the kick-off
            ActionUtility.Invoke_Action(Owner.OnTakeKickOff);
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
