using System;
using System.Collections.Generic;
using Core.GameFlow.StateMachine.Interfaces;
using UnityEngine;

namespace Core.GameFlow.StateMachine
{
    public class GameFlowStateMachineMock : MonoBehaviour, IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public void Start()
        { 
           _states = new Dictionary<Type, IState>();
           _states.Add(typeof(GameplayStateMock), new GameplayStateMock(this));
           EnterState<GameplayStateMock>();
        }
        
        public void EnterState<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
            Debug.LogWarning($"Entered state {_currentState.GetType().Name}");
        }
    }
}
