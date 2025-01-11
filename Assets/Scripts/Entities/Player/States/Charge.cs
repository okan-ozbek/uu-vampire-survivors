using Configs;
using Controllers;
using Controllers.Player;
using UnityEngine;

namespace Entities.Player.States
{
    public class Charge : PlayerState
    {
        private const float ChargeTime = 1f;
        private float _shakeIntensity = 0.00001f;
        
        private bool _isFinished;
        private int _chargeLevel;
        
        // TODO: This would be better in as a substate of Run
        private Vector2 _velocity;

        private readonly TimeController _timer;
        
        public Charge(PlayerCore core) : base(core)
        {
            _timer = new TimeController(ChargeTime);
        }

        public override void Enter()
        {
            // TODO: This would be better in as a substate of Run
            _velocity = Core.Body.linearVelocity;
            _isFinished = false;
        }

        public override void Update()
        {
            Decelerate();
            Core.Body.linearVelocity = _velocity;

            _isFinished = PlayerInputController.AttackKeyReleased;
            
            ChargeTimer();
            ShakeTransform();
        }

        public override void Exit()
        {
            ResetParameters();
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(BasicAttack), () => _isFinished && _chargeLevel == 0);
            AddTransition(typeof(HeavyAttack), () => _isFinished && _chargeLevel == 1);
        }

        private void ResetParameters()
        {
            _isFinished = false;
            _chargeLevel = 0;
            _shakeIntensity = 0.00001f;
            
            _timer.Reset();
        }

        private void ChargeTimer()
        {
            if (_isFinished == false)
            {
                _timer.Update();
                _shakeIntensity += 0.00001f;
                if (_timer.IsFinished() && _chargeLevel == 0)
                {
                    _chargeLevel++;
                    PlayerEventConfig.OnPlayerChargeAttack?.Invoke(Core.Data.Guid, _chargeLevel);
                }
            }
        }

        private void ShakeTransform()
        {
            Vector3 originalPosition = Core.transform.position;
            float offsetX = Random.Range(-_shakeIntensity, _shakeIntensity);
            float offsetY = Random.Range(-_shakeIntensity, _shakeIntensity);
            Core.transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);
        }
        
        // TODO: This would be better in as a substate of Run
        #region Make substate of run
        private void Decelerate()
        {
            Debug.Log(PlayerInputController.MovementDirection);
            
            if (Mathf.Abs(Core.Body.linearVelocity.x) > 0.0f)
            {
                SetMoveTowardsX(Core.Data.decelerationSpeed * Time.deltaTime);
            }
            
            if (Mathf.Abs(Core.Body.linearVelocity.y) > 0.0f)
            {
                SetMoveTowardsY(Core.Data.decelerationSpeed * Time.deltaTime);
            }
        }
        
        private void SetMoveTowardsX(float maxDelta)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0.0f, maxDelta);   
        }
        
        private void SetMoveTowardsY(float maxDelta)
        {
            _velocity.y = Mathf.MoveTowards(_velocity.y, 0.0f, maxDelta);   
        }
        #endregion
    }
}