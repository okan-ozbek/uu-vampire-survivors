using System;
using Unity;
using UnityEngine;

namespace Serializables
{
    [Serializable]
    public class PlayerData
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
        public float runSpeed;
        public float chargeSpeed;

        [Header("Dash")]
        public float dashPower;
        public float dashDuration;
        public float dashCooldown;
        public bool canDash;
    }
}