using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerAudioConfig", menuName = "Player/Audio Config")]
    public class PlayerAudioConfig : ScriptableObject
    {
        public AudioClip[] playerDashSFX;
        public AudioClip[] playerAttackSFX;
        public AudioClip[] playerChargeSFX;
    }
}