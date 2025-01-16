using System;
using UnityEngine;

namespace Utility
{
    public class Timer
    {
        public event Action OnTimerEnd;

        public float Duration { get; }
        public float TimePassed { get; private set; }
        public bool Completed { get; private set; }

        private readonly bool _loop;

        public Timer(float duration, bool loop = false)
        {
            Duration = duration;
            TimePassed = 0f;
            
            _loop = loop;
        }

        public void Update() 
        {
            if (Completed)
            {
                return;
            }

            TimePassed += Time.deltaTime;
            if (TimePassed >= Duration)
            {
                if (Completed)
                {
                    return;
                }
                
                OnTimerEnd?.Invoke();
                if (_loop == false)
                {
                    Completed = true;
                }
            }
        } 

        public void Reset()
        {
            TimePassed = 0f;
            Completed = false;
        }
    }
}