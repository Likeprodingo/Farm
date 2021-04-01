using System;
using System.Collections;
using DefaultNamespace;
using GamePlay.AnimationState;
using GamePlay.AnimationState.Impl;
using UnityEngine;

namespace GamePlay.PlayerActions
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private StateManager _stateManager = default;
        [SerializeField] private Animator _animator = default;

        private const string ANIMATION_PARAMETR = "animation";
        
        private MoveState _state;
        private void OnEnable()
        {
            StateManager.StateUpdated += MoveStateManagerOnStateUpdated;
        }
        
        private void OnDisable()
        {
            StateManager.StateUpdated -= MoveStateManagerOnStateUpdated;
        }

        private void MoveStateManagerOnStateUpdated(MoveState obj)
        {
            _animator.SetInteger(ANIMATION_PARAMETR,(int) obj.PlayerState);
            _state = obj;
        }

        public void UpdateState()
        {
            _stateManager.CompleteState(_state);
        }
    }
}