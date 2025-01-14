using Configs;
using Controllers;
using UnityEngine;

namespace Entities.Player.States
{
    public class PlayerDash : PlayerState
    {
        private readonly TimeController _timeController;
        private Vector3 _direction;
        
        public PlayerDash(PlayerCore core) : base(core)
        {
            Debug.Log("Called");
            _timeController = new TimeController(0.1f);
        }
        
        protected override void OnEnter()
        {
            PlayerEventConfig.OnPlayerDash.Invoke(Core.Data.Guid);
            _direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f).normalized;
        }

        protected override void OnExit()
        {
            Debug.Log("called exit");
            _timeController.Reset();
        }

        protected override void OnUpdate()
        {
            _timeController.Update();
            if (_timeController.IsFinished() == false)
            {
                Core.transform.Translate(_direction * 30f * Time.deltaTime);
            }
            
            
            //Debug.Log($"{_timeController.Duration}, {_timeController.TimePassed}, {_timeController.IsFinished()}");
        }

        protected override void SetTransitions()
        {
            AddTransition(typeof(PlayerIdle), () => _timeController.IsFinished());
        }

        protected override void SetChildTransitions()
        {
        }
    }
}