using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerEventConfig", menuName = "Player/Event Config")]
    public class PlayerEventConfig : ScriptableObject
    {
        public static Action<Guid> OnPlayerDash;
        public static Action<Guid> OnPlayerBasicAttack;
        public static Action<Guid> OnPlayerHeavyAttack;
        public static Action<Guid, int> OnPlayerChargeAttack;
    }
}