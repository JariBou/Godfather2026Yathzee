using System;
using _project.Scripts.Die;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.DieLaunching
{
    public class LaunchedDieBroadcaster : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [FormerlySerializedAs("_dice"),SerializeField] private DiceBase _diceBase;
        public event Action<DiceBase> StoppedMoving;
        private bool _isMoving;

        private void Reset()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
            _diceBase ??= GetComponent<DiceBase>();
        }

        public void WasLaunched()
        {
            _isMoving = true;
            _rigidbody.isKinematic = false;
        }

        private void FixedUpdate()
        {
            if (_rigidbody == null)
            {
                throw new MissingComponentException($"Missing rigidbody on {nameof(LaunchedDieBroadcaster)}");
            }

            if (_rigidbody.linearVelocity.magnitude < 0.01f && _isMoving)
            {
                _isMoving = false;
                StoppedMoving?.Invoke(_diceBase);
                _rigidbody.isKinematic = true;
            }
        }
    }
}