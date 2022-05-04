﻿using System;
using Patterns.Singleton;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource _ballKickAS;

        public AudioSource _goalAS;

        public AudioSource _ambience;

        public void PlayBallKickedSound(float flightTime, float velocity, Vector3 initial, Vector3 target)
        {
            _ballKickAS.Play();
        }

        public void PlayGoalScoredSound(string message, string message1)
        {
            _goalAS.Play();
        }
    }
}
