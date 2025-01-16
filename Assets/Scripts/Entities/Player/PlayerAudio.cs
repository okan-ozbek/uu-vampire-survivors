using System;
using Configs;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Player
{
    public class PlayerAudio : AudioController
    {
        [SerializeField] private PlayerAudioConfig playerAudioConfig;
        
        private void OnEnable()
        {
            PlayerEventConfig.OnPlayerDash += HandleOnPlayerDash;
            PlayerEventConfig.OnPlayerBasicAttack += HandleOnPlayerBasicAttack;
            PlayerEventConfig.OnPlayerHeavyAttack += HandleOnPlayerHeavyAttack;
            PlayerEventConfig.OnPlayerChargeAttack += HandleOnPlayerChargeAttack;
        }

        private void OnDisable()
        {
            PlayerEventConfig.OnPlayerDash -= HandleOnPlayerDash;
            PlayerEventConfig.OnPlayerBasicAttack -= HandleOnPlayerBasicAttack;
            PlayerEventConfig.OnPlayerHeavyAttack -= HandleOnPlayerHeavyAttack;
            PlayerEventConfig.OnPlayerChargeAttack -= HandleOnPlayerChargeAttack;
        }

        private void HandleOnPlayerDash(Guid guid) => PlayAudio(playerAudioConfig.playerDashSFX, Random.Range(0.7f, 1.3f));
        private void HandleOnPlayerBasicAttack(Guid guid) => PlayAudio(playerAudioConfig.playerAttackSFX, Random.Range(0.75f, 1.25f));
        private void HandleOnPlayerHeavyAttack(Guid guid) => PlayAudio(playerAudioConfig.playerAttackSFX, Random.Range(0.25f, 0.5f));
        
        private void HandleOnPlayerChargeAttack(Guid guid, int chargeLevel)
        {
            PlayAudio(playerAudioConfig.playerChargeSFX, 1f * (chargeLevel / 4f));   
        }
    }
}