using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarecrow.DesignPatterns.StateMachine
{
    public abstract class TimedTransitionState : BaseState
    {
        protected abstract Enum StateToTransitionTo { get; set; }
        [field: SerializeField] 
        public float SecondsUntilStateTransition { get; protected set; } = 1f;
        public float SecondsLeft { get; private set; }

        private bool IsPaused = false;

        public override void OnEnter()
        {
            base.OnEnter();
            SecondsLeft = SecondsUntilStateTransition;
        }
        
        protected virtual void Update()
        {
            if (IsPaused)
            {
                return;
            }
            
            if (SecondsLeft > 0)
            {
                SecondsLeft -= Time.deltaTime;
            }
            else
            {
                OwningStateMachine?.ChangeState(StateToTransitionTo);
            }
        }

        protected void PauseUpdate() => IsPaused = true;

        protected void ResumeUpdate() => IsPaused = false;
    }
}
