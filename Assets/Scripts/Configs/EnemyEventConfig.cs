using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyEventConfig", menuName = "Enemy/Event Config")]
    public class EnemyEventConfig : ScriptableObject
    {
        public static Action<Guid> OnEnemyHurt;
        public static Action<Guid> OnEnemyFootstep;
        public static Action<Guid> OnEnemyDeath;
    }
}