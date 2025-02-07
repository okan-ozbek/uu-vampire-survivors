using System;
using Configs;
using Controllers;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerAudio : AudioController
    {
        [SerializeField] private PlayerConfig config;
        
        private void OnEnable()
        {
            PlayerConfig.OnPlayDashSFX += HandleOnPlayDashSFX;
            PlayerConfig.OnPlayHurtSFX += HandleOnPlayHurtSFX;
            PlayerConfig.OnPlayFootstepSFX += HandleOnPlayFootstepSFX;
            PlayerConfig.OnPlayAttackSFX += HandleOnPlayAttackSFX;
        }

        private void OnDisable()
        {
            PlayerConfig.OnPlayDashSFX -= HandleOnPlayDashSFX;
            PlayerConfig.OnPlayHurtSFX -= HandleOnPlayHurtSFX;
            PlayerConfig.OnPlayFootstepSFX -= HandleOnPlayFootstepSFX;
            PlayerConfig.OnPlayAttackSFX -= HandleOnPlayAttackSFX;
        }
        
        private void HandleOnPlayDashSFX() => PlayAudio(config.playerDashSFX);
        private void HandleOnPlayHurtSFX() => PlayAudio(config.playerHurtSFX);
        private void HandleOnPlayFootstepSFX() => PlayAudio(config.playerFootstepSFX);
        private void HandleOnPlayAttackSFX() => PlayAudio(config.playerAttackSFX);
    }
}