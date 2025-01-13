﻿using System.Collections;
using Configs;
using UnityEngine;

namespace Entities.Player.States
{
    public class HeavyAttack : PlayerState
    {
        private bool _isFinished;
        
        public HeavyAttack(PlayerCore core, PlayerFactory factory) : base(core, factory)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerEventConfig.OnPlayerHeavyAttack?.Invoke(Core.Data.Guid);
            Core.StartCoroutine(AttackRoutine());
        }
        
        protected override void SetTransitions()
        {
            AddTransition(typeof(Wield), () => _isFinished);
            // AddTransition(typeof(Run), () => _isFinished && Core.Body.linearVelocity != Vector2.zero);
            // AddTransition(typeof(Idle), () => _isFinished);
        }
        
        private IEnumerator AttackRoutine()
        {
            _isFinished = false;
            Core.SwordHitbox.transform.localScale = Vector3.one * 2f;
            Core.SwordHitbox.SetActive(true);
            
            yield return new WaitForSeconds(0.2f);
            
            Core.SwordHitbox.SetActive(false);
            Core.SwordHitbox.transform.localScale = Vector3.one;
            _isFinished = true;
        }
    }
}