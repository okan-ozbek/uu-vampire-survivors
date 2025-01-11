using UnityEngine;

namespace Controllers
{
    public class TimeController
    {
        private float Duration { get; }

        private float _timePassed;
        
        public TimeController(float duration)
        {
            Duration = duration;
        }

        public void Update()
        {
            _timePassed += Time.deltaTime;
        }
        
        public bool IsFinished()
        {
            return _timePassed > Duration;
        }

        public void Reset()
        {
            _timePassed = 0;
        }
    }
}