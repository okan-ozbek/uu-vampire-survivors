using System.Collections;
using Configs;
using Controllers;
using Controllers.Player;
using Serializables;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private GameObject swordHitbox;
        
        private Rigidbody2D Body { get; set; }
        private PlayerData Data => data;

        private TimeController _timer;
        private Vector3 _velocity = Vector3.zero;
        private bool _dashing = false;
        private bool _charging = false;
        private int _level = 0;
        private float _reduceSpeedBufferTime = 0.1f;

        private float _baseMaxSpeed;
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
            _timer = new TimeController(1f);

            Data.Initialize();
            Debug.Log(Data.BaseMaxSpeed);
        }

        private void Update()
        {
            if (PlayerInputController.AttackKeyPressed || PlayerInputController.AttackKeyHeld)
            {
                _charging = true;
                ChargeTimer();

                if (_timer.TimePassed > _reduceSpeedBufferTime && _baseMaxSpeed != Data.maxSpeed) 
                {
                    Data.maxSpeed *= 0.3f;
                }
            }
            
            if (PlayerInputController.AttackKeyReleased)
            {
                _charging = false;
                Data.maxSpeed = Data.BaseMaxSpeed;
                _level = 0;

                if (_timer.IsFinished())
                {
                    PlayerEventConfig.OnPlayerHeavyAttack?.Invoke(Data.Guid);
                    StartCoroutine(AttackRoutine(Vector3.one * 2f));
                }
                else
                {
                    PlayerEventConfig.OnPlayerBasicAttack?.Invoke(Data.Guid);
                    StartCoroutine(AttackRoutine(Vector3.one));
                }

                _timer.Reset();
            }
            
            if (PlayerInputController.DashKeyPressed && Data.canDash)
            {
                _dashing = true;
                
                PlayerEventConfig.OnPlayerDash?.Invoke(Data.Guid);
                StartCoroutine(DashRoutine());
                StartCoroutine(DashCooldown());
            }

            if (_dashing == false)
            {
                Movement();
            }

            Flip();
        }

        private void Flip()
        {
            Vector3 localScale = transform.localScale;

            localScale.x = Body.linearVelocity.x switch
            {
                > 0.0f => Mathf.Abs(localScale.x),
                < 0.0f => -Mathf.Abs(localScale.x),
                _ => localScale.x
            };

            transform.localScale = localScale;
        }
        
        private void Movement()
        {
            Accelerate();
            Decelerate();
            Brake();
            
            Body.linearVelocity = _velocity;
        }
        
        private void Accelerate()
        {
            if (PlayerInputController.MovementDirection.x != 0)
            {
                if (Mathf.Abs(Body.linearVelocity.x) > Data.maxSpeed)
                {
                    SetMoveTowardsX(Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.x += PlayerInputController.NormalizedMovementDirection.x * Data.accelerationSpeed * Time.deltaTime;
                }
            }
            
            if (PlayerInputController.MovementDirection.y != 0)
            {
                if (Mathf.Abs(Body.linearVelocity.y) > Data.maxSpeed)
                {
                    SetMoveTowardsY(Data.decelerationSpeed * Time.deltaTime);
                }
                else
                {
                    _velocity.y += PlayerInputController.NormalizedMovementDirection.y * Data.accelerationSpeed * Time.deltaTime;
                }
            }
        }

        private void Decelerate()
        {
            if (PlayerInputController.MovementDirection.x == 0)
            {
                SetMoveTowardsX(Data.decelerationSpeed * Time.deltaTime);
            }
            
            if (PlayerInputController.MovementDirection.y == 0)
            {
                SetMoveTowardsY(Data.decelerationSpeed * Time.deltaTime);
            }
        }

        private void Brake()
        {
            if (PlayerInputController.MovementDirection.x != 0 && PlayerInputController.MovementDirection.x != Mathf.Sign(_velocity.x))
            {
                SetMoveTowardsX(Data.brakeSpeed * Time.deltaTime);
            }
            
            if (PlayerInputController.MovementDirection.y != 0 && PlayerInputController.MovementDirection.y != Mathf.Sign(_velocity.y))
            {
                SetMoveTowardsY(Data.brakeSpeed * Time.deltaTime);
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
        
        private IEnumerator DashRoutine()
        {
            Body.linearVelocity = PlayerInputController.MovementDirection * Data.dashPower;
            
            yield return new WaitForSeconds(Data.dashDuration);
            
            _dashing = false;
        }
        
        private IEnumerator DashCooldown()
        {
            Data.canDash = false;
            PlayerEventConfig.OnDashCooldown?.Invoke(Data.Guid, Data.dashCooldown);
            
            yield return new WaitForSeconds(Data.dashCooldown);
            
            Data.canDash = true;
        }

        private void ChargeTimer()
        {
            if (_charging)
            {
                _timer.Update();
                
                if (_timer.IsFinished() && _level == 0)
                {
                    _level = 1;
                    PlayerEventConfig.OnPlayerChargeAttack?.Invoke(Data.Guid, _level);
                }
            }
        }
        
        private IEnumerator AttackRoutine(Vector3 scale)
        {
            swordHitbox.transform.localScale = scale;
            swordHitbox.SetActive(true);
            
            yield return new WaitForSeconds(0.2f);
            
            swordHitbox.SetActive(false);
            swordHitbox.transform.localScale = scale;
        }
    }
}