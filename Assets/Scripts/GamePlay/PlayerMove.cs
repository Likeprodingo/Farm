using System;
using DefaultNamespace;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Animator _animator = default;

        [SerializeField] private AnimationData _animationKeySet = default;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Joystick _joystick = default;
        [SerializeField] private Rigidbody _rigidbody = default;
        
        private void FixedUpdate()
        {
            var dir = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _rigidbody.velocity = dir * _speed;
            if (_joystick.Direction != Vector2.zero)
            {
                transform.LookAt(transform.position + dir);
                _animator.SetInteger(_animationKeySet._paramName, _animationKeySet._walk);
            }
            else
            {
                _animator.SetInteger(_animationKeySet._paramName, _animationKeySet._idle);
            }
        }
    }
}