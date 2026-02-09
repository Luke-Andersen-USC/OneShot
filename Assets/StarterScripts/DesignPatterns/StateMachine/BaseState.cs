using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarecrow.DesignPatterns.StateMachine
{
    public abstract class BaseState : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Reference to the owning state machine")]
        [SerializeField] protected FiniteStateMachine OwningStateMachine;

        [Tooltip("References to the behaviors to activate in this state")]
        [SerializeField] protected List<MonoBehaviour> BehaviorsToActivateInState;
        
        public abstract Enum StateEnum { get; }

        protected virtual void Awake()
        {
            OwningStateMachine.RegisterState(this);
        }

        // TODO: make sure OnEnable() and OnDisable are not called at Start(), when all states are activated and then deactivated.
        //       This can also be fixed by only marking the default state gameobject as active in the prefab while still supporting state self registration
        public virtual void OnEnter()
        {
            SetBehaviorsActive(true);
        }

        public virtual void OnExit()
        {
            SetBehaviorsActive(false);
        }

        private void SetBehaviorsActive(bool value)
        {
            if (BehaviorsToActivateInState == null)
            {
                return;
            }
            
            foreach (MonoBehaviour behavior in BehaviorsToActivateInState)
            {
                if (behavior)
                {
                    behavior.enabled = value;
                }
            }
        }
    }
}
