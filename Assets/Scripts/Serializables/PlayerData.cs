using System;
using Unity;
using UnityEngine;

namespace Serializables
{
    [Serializable]
    public class PlayerData : EntityData
    {
        [Header("Movement")]
        [StepRange(1.0f, 100.0f, 5.0f)]  public float power;
        [StepRange(1.0f, 200.0f, 10.0f)] public float accelerationSpeed;
        [StepRange(1.0f, 200.0f, 10.0f)] public float decelerationSpeed;
        [StepRange(1.0f, 20.0f, 1.0f)]   public float maxSpeed;
        [StepRange(1.0f, 100.0f, 5.0f)]  public float brakeSpeed;

        [Header("Dash")]
        [StepRange(1.0f, 100.0f, 1.0f)] public float dashPower;
        [StepRange(0.1f, 5.0f, 0.1f)]   public float dashDuration;
        [StepRange(1.0f, 10.0f, 1.0f)]  public float dashCooldown;
        public bool canDash;
        
        public float BaseMaxSpeed { get; private set; }

        public void Initialize()
        {
            BaseMaxSpeed = maxSpeed;
        }
    }
}