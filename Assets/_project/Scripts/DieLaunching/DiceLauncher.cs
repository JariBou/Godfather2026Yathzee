using System.Collections.Generic;
using _project.Scripts.Die;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts.DieLaunching
{
    public class DiceLauncher : MonoBehaviour
    {
        [SerializeField, RequiredType(typeof(Rigidbody))] private GameObject _dicePrefab;
        [SerializeField, Range(0f, 45f)] private float _randAngleMax = 25f;
        [SerializeField, MinMaxSlider(0.5f, 50f)] private Vector2 _randForce = new (25f, 30f);
        [SerializeField, MinMaxSlider(0.5f, 50f)] private Vector2 _randAngularForce = new (0f, 25f);
        [SerializeField] private float _waveLaunchDelay = 0.3f;
        
        [SerializeField, Foldout("DEBUG")] private int _maxDiceCount = 50;
        [SerializeField, Foldout("DEBUG")] private int _waveLaunchCount = 5;
        private Queue<GameObject> _diceQueue = new();

        [Button]
        public void ClearDices()
        {
            while (_diceQueue.Count > 0)
            {
                Destroy(_diceQueue.Dequeue());
            }
        }

        public void LaunchDice(GameObject dicePrefab)
        {
            if (_diceQueue.Count > _maxDiceCount)
            {
                Destroy(_diceQueue.Dequeue());
            }
            GameObject dice = Instantiate(dicePrefab, transform.position, Random.rotation, transform);
            _diceQueue.Enqueue(dice);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.angularVelocity = Random.onUnitSphere * Random.Range(_randAngularForce.x, _randAngularForce.y);
            
            if (rb == null)
            {
                throw new MissingComponentException("Missing rigidbody on Dice Prefab");
            }
            
            Vector3 direction = Quaternion.AngleAxis(Random.Range(-_randAngleMax, _randAngleMax), transform.forward)
                                * (Quaternion.AngleAxis(Random.Range(-_randAngleMax, _randAngleMax), transform.right) * transform.up);
            rb.linearVelocity = direction * Random.Range(_randForce.x, _randForce.y);
        }
        
        [Button]
        public void LaunchDice()
        {
            if (_diceQueue.Count > _maxDiceCount)
            {
                Destroy(_diceQueue.Dequeue());
            }
            GameObject dice = Instantiate(_dicePrefab, transform.position, Random.rotation, transform);
            _diceQueue.Enqueue(dice);
            
            LaunchedDieBroadcaster launchedDieBroadcaster = dice.GetComponent<LaunchedDieBroadcaster>();
            if (launchedDieBroadcaster == null)
            {
                throw new MissingComponentException("Missing LaunchedDieBroadcaster on Dice Prefab");
            }
            launchedDieBroadcaster.WasLaunched();
            launchedDieBroadcaster.StoppedMoving += LaunchedDieBroadcasterOnStoppedMoving;
            
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            if (rb == null)
            {
                throw new MissingComponentException("Missing rigidbody on Dice Prefab");
            }

            rb.angularVelocity = Random.onUnitSphere * Random.Range(_randAngularForce.x, _randAngularForce.y);
            
            Vector3 direction = Quaternion.AngleAxis(Random.Range(-_randAngleMax, _randAngleMax), transform.forward)
                                * (Quaternion.AngleAxis(Random.Range(-_randAngleMax, _randAngleMax), transform.right) * transform.up);
            rb.linearVelocity = direction * Random.Range(_randForce.x, _randForce.y);
        }

        private void LaunchedDieBroadcasterOnStoppedMoving(Dice dice)
        {
            Debug.Log($"A Die has stopped moving, face value is: {dice.GetUpFace()}");
        }

        [Button]
        public void WaveLaunchDice()
        {
            _ = DelayedWaveLaunch();
        }

        private async Awaitable DelayedWaveLaunch()
        {
            for (int i = 0; i < _waveLaunchCount; i++)
            {
                LaunchDice();
                await Awaitable.WaitForSecondsAsync(_waveLaunchDelay);
            }
        }

        private void OnDrawGizmos()
        {
            // float angle = _randAngleMax*2;
            // float rayRange = 10.0f;
            // float halfFOV = angle / 2.0f;
            // float coneDirection = 180;
            //
            // Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.up);
            // Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.up);
            //
            // Vector3 upRayDirection = upRayRotation * transform.right * rayRange;
            // Vector3 downRayDirection = downRayRotation * transform.right * rayRange;
            //
            // Gizmos.DrawRay(transform.position, upRayDirection);
            // Gizmos.DrawRay(transform.position, downRayDirection);
            // Gizmos.DrawLine(transform.position + downRayDirection, transform.position + upRayDirection);
            
            //
            // float angle = Vector3.Angle(transform.forward, transform.up);
            // // Quaternion.AngleAxis(angle, transform.right);
            //
            // Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.AngleAxis(angle, transform.right), Vector3.one);
            // Gizmos.DrawFrustum(Vector3.zero, _randAngleMax, 10f, 0f, 1f);
            // Gizmos.matrix = Matrix4x4.identity;
            // // Gizmos.DrawFrustum(transform.position, _randAngleMax, 50f, 0f, 16/9f);
            //

            Color color = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 5f);
            Gizmos.color = color;
        }
    }
}