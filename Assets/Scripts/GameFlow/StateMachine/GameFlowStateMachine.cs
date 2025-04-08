using System;
using System.Collections.Generic;
using GameFlow.StateMachine.Interfaces;
using UnityEngine;

namespace GameFlow.StateMachine
{
    public class GameFlowStateMachine : MonoBehaviour, IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public void Start()
        { 
           _states = new Dictionary<Type, IState>();
           _states.Add(typeof(BootstrapperState), new BootstrapperState(this));
           _states.Add(typeof(MainMenuState), new MainMenuState(this));
           _states.Add(typeof(GameplayState), new GameplayState(this));
           
           EnterState<BootstrapperState>();
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
