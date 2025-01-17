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
        
        private int _currentFootstepIndex;
        
        private void OnEnable()
        {
            PlayerEventConfig.OnPlayerDash += HandleOnPlayerDash;
            PlayerEventConfig.OnPlayerHurt += HandleOnPlayerHurt;
            PlayerEventConfig.OnPlayerAttack += HandleOnPlayerAttack;
            PlayerEventConfig.OnPlayerAttackEnd += HandleOnPlayerAttackEnd;
            PlayerEventConfig.OnPlayerFootstep += HandleOnPlayerFootstep;
        }

        private void OnDisable()
        {
            PlayerEventConfig.OnPlayerDash -= HandleOnPlayerDash;
            PlayerEventConfig.OnPlayerHurt -= HandleOnPlayerHurt;
            PlayerEventConfig.OnPlayerAttack -= HandleOnPlayerAttack;
            PlayerEventConfig.OnPlayerAttackEnd -= HandleOnPlayerAttackEnd;
            PlayerEventConfig.OnPlayerFootstep -= HandleOnPlayerFootstep;
        }

        private void HandleOnPlayerDash(Guid guid) => PlayAudio(playerAudioConfig.playerDashSFX, Random.Range(0.7f, 1.3f));
        private void HandleOnPlayerHurt(Guid guid) => PlayAudio(playerAudioConfig.playerHurtSFX, Random.Range(0.7f, 1.3f));
        private void HandleOnPlayerAttack(Guid guid) => PlayAudio(playerAudioConfig.playerAttackSFX[0], Random.Range(0.7f, 1.3f), 1.5f);
        private void HandleOnPlayerAttackEnd(Guid guid) => PlayAudio(playerAudioConfig.playerAttackSFX[1], Random.Range(0.7f, 1.3f));

        private void HandleOnPlayerFootstep(Guid guid)
        {
            _currentFootstepIndex ^= 1;
            float pitch = (_currentFootstepIndex == 0)
                ? Random.Range(0.5f, 0.85f)
                : Random.Range(1.15f, 1.5f);
            
            PlayAudio(playerAudioConfig.playerFootstepSFX, pitch, 0.15f);
        }
    }
}