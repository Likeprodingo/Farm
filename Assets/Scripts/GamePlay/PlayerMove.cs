using System;
using UnityEngine;

namespace GamePlay
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private Joystick _joystick = default;
        [SerializeField] private Rigidbody _rigidbody = default;
        
        private void FixedUpdate()
        {
            _rigidbody.AddForce(_joystick.Direction*_speed);
        }
    }
}