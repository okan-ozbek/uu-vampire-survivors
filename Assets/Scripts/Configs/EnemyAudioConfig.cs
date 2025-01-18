using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyAudioConfig", menuName = "Enemy/Audio Config")]
    public class EnemyAudioConfig : ScriptableObject
    {
        public AudioClip[] enemyHurtSFX;
        public AudioClip[] enemyDeathSFX;
        public AudioClip[] enemyFootstepSFX;
    }
}