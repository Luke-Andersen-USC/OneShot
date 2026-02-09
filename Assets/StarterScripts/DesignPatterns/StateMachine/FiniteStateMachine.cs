using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scarecrow.DesignPatterns.StateMachine
{
    public abstract class FiniteStateMachine : MonoBehaviour
    {
        private readonly Dictionary<Enum, BaseState> StatesMap  = new();

        public abstract Enum CurrentState { get; protected set; }
        
        protected virtual void Start()
        {
            // Set all state gameobjects as inactive since we only want the current state gameobject to be active
            foreach (BaseState state in StatesMap.Values)
            {
                state.gameObject.SetActive(false);
            }
        }

        public void RegisterState(BaseState state)
        {
            // The state must have a UNIQUE gameobject associated with it
            Assert.IsNotNull(state.gameObject);
            Assert.IsTrue(state.gameObject != this.gameObject);
            
            StatesMap.Add(state.StateEnum, state);
        }
        
        public virtual void ChangeState(Enum nextStateEnum)
        {
            Assert.IsTrue(StatesMap.Count > 0);
            Assert.IsTrue(StatesMap.ContainsKey(nextStateEnum));
            Assert.IsTrue(StatesMap.ContainsKey(CurrentState));    
            
            // Deactivate current state
            BaseState currentState = StatesMap[CurrentState];
            currentState.OnExit();
            currentState.gameObject.SetActive(false);
            
            // Activate next state
            CurrentState = nextStateEnum;
            BaseState nextState = StatesMap[nextStateEnum];
            nextState.gameObject.SetActive(true);
            nextState.OnEnter();
        }

        public BaseState GetState(Enum state)
        {
            if (StatesMap.ContainsKey(state))
            {
                return StatesMap[state];
            }
            return null;
        }
    }
}
