using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "enemy_config", menuName = "Entity/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public static Action OnPlayEnemyHurtSFX;
        public static Action OnPlayEnemyDeathSFX;
        public static Action OnPlayEnemyFootstepSFX;
        
        public AudioClip[] enemyHurtSFX;
        public AudioClip[] enemyDeathSFX;
        public AudioClip[] enemyFootstepSFX;
    }
}