using System;
using Configs;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemies
{
    public class EnemyAudio : AudioController
    {
        [SerializeField] private EnemyAudioConfig enemyAudioConfig;
        
        private int _currentFootstepIndex;
        
        private void OnEnable()
        {
            EnemyEventConfig.OnEnemyHurt += HandleOnPlayerHurt;
            EnemyEventConfig.OnEnemyFootstep += HandleOnPlayerFootstep;
            EnemyEventConfig.OnEnemyDeath += HandleOnEnemyDeath;
        }

        private void OnDisable()
        {
            EnemyEventConfig.OnEnemyHurt -= HandleOnPlayerHurt;
            EnemyEventConfig.OnEnemyFootstep -= HandleOnPlayerFootstep;
            EnemyEventConfig.OnEnemyDeath -= HandleOnEnemyDeath;
        }
        
        private void HandleOnPlayerHurt(Guid guid) => PlayAudio(enemyAudioConfig.enemyHurtSFX, Random.Range(0.2f, 0.6f));
        private void HandleOnEnemyDeath(Guid guid) => PlayAudio(enemyAudioConfig.enemyDeathSFX, Random.Range(0.9f, 1.1f));
        
        private void HandleOnPlayerFootstep(Guid guid)
        {
            _currentFootstepIndex ^= 1;
            float pitch = (_currentFootstepIndex == 0)
                ? Random.Range(0.2f, 0.55f)
                : Random.Range(0.85f, 1.15f);
            
            PlayAudio(enemyAudioConfig.enemyFootstepSFX, pitch, 0.15f);
        }
    }
}