using Patterns.Singleton;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Pitch : Singleton<Pitch>
    {
        [SerializeField]
        Transform _centerSpot;

        public Transform CenterSpot { get => _centerSpot; set => _centerSpot = value; }
    }
}
