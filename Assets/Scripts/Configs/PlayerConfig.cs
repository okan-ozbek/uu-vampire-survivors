using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "player_config", menuName = "Entity/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public static Action OnPlayDashSFX;
        public static Action OnPlayHurtSFX;
        public static Action OnPlayFootstepSFX;
        public static Action OnPlayAttackSFX;
        
        public AudioClip[] playerDashSFX;
        public AudioClip[] playerHurtSFX;
        public AudioClip[] playerFootstepSFX;
        public AudioClip[] playerAttackSFX;
    }
}