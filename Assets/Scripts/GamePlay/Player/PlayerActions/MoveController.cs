using System;
using GamePlay.AnimationState;
using GamePlay.AnimationState.Impl;
using UnityEngine;

namespace GamePlay.PlayerActions
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Joystick _joystick = default;
        [SerializeField] private Rigidbody _rigidbody = default;

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
            _state = obj;
        }
        
        public void FixedUpdate()
        {
            var direction = _joystick.Direction;
            if (_state.Move(direction))
            {
                var dir = new Vector3(direction.x, 0, direction.y);
                _rigidbody.velocity = dir * _speed;
                transform.LookAt(transform.position + dir);
            }
        }
    }
}