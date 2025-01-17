using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerEventConfig", menuName = "Player/Event Config")]
    public class PlayerEventConfig : ScriptableObject
    {
        public static Action<Guid> OnPlayerDash;
        public static Action<Guid, float> OnDashCooldown;
        public static Action<Guid> OnPlayerHurt;
        public static Action<Guid> OnPlayerAttack;
        public static Action<Guid> OnPlayerAttackEnd;
        public static Action<Guid> OnPlayerFootstep;
    }
}