using UnityEngine;

namespace Controllers
{
    public class TimeController
    {
        public float Duration { get; }
        public float TimePassed { get; private set; }

        public TimeController(float duration)
        {
            Duration = duration;
            TimePassed = 0f;
        }

        public void Update()
        {
            TimePassed += Time.deltaTime;
        }

        public bool IsFinished()
        {
            return TimePassed >= Duration;
        }

        public void Reset()
        {
            TimePassed = 0f;
        }
    }
}