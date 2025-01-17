using System;
using UnityEngine;

namespace Animations
{
    public class SimpleAnimationStateBehaviour : StateMachineBehaviour
    {
        public static event Action<string> OnAnimationCompleted;
        public static event Action<string> OnAnimationTriggerActivated;
        
        [SerializeField] [Range(0f, 1f)] private float triggerEventAt;
        
        private bool TriggeredEvent { get; set; }
        private bool Completed { get; set; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            TriggeredEvent = false;
            Completed = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float currentNormalizedTime = stateInfo.normalizedTime % 1f;
            float wiggleRoom = 0.05f;
            
            if (currentNormalizedTime >= triggerEventAt && TriggeredEvent == false)
            {
                TriggeredEvent = true;
                OnAnimationTriggerActivated?.Invoke(name);
            }
            
            if (currentNormalizedTime >= 1f - wiggleRoom && Completed == false)
            {
                Completed = true;
                OnAnimationCompleted?.Invoke(name);
            }
        }
    
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            TriggeredEvent = false;
            Completed = false;
        }
    }
}
