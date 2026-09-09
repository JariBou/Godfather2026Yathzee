using System;
using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts.DieLaunching
{
    public class LaunchedDieBroadcaster : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Dice _dice;
        public event Action<Dice> StoppedMoving;
        private bool _isMoving;

        private void Reset()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
            _dice ??= GetComponent<Dice>();
        }

        public void WasLaunched()
        {
            _isMoving = true;
        }

        private void FixedUpdate()
        {
            if (_rigidbody == null)
            {
                throw new MissingComponentException($"Missing rigidbody on {nameof(LaunchedDieBroadcaster)}");
            }

            if (_rigidbody.linearVelocity.magnitude < 0.1f && _isMoving)
            {
                _isMoving = false;
                StoppedMoving?.Invoke(_dice);
            }
        }
    }
}