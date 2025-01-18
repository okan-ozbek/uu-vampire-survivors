using System;
using Unity;
using UnityEngine;

namespace Serializables
{
    [Serializable]
    public class EnemyData
    {
        public Guid Guid { get; } = Guid.NewGuid();

        [Header("Stats")]
        public float health;
        public float stamina;

        [Header("Movement")]
        public float accelerationSpeed;
        public float decelerationSpeed;
        public float brakeSpeed;
        public float maxSpeed;
        public float walkSpeed;

        [Header("State management")] 
        public bool isHurt;
    }
}