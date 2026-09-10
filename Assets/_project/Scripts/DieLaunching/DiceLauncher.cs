using System.Collections.Generic;
using _project.Scripts.Die;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts.DieLaunching
{
    public class DiceLauncher : MonoBehaviour
    {
        [SerializeField, RequiredType(typeof(Rigidbody))] private DiceBase _dicePrefab;
        [SerializeField, Range(0f, 45f)] private float _randAngleMax = 25f;
        [SerializeField, MinMaxSlider(0.5f, 50f)] private Vector2 _randForce = new (25f, 30f);
        [SerializeField, MinMaxSlider(0.5f, 50f)] private Vector2 _randAngularForce = new (0f, 25f);
        [SerializeField] private float _waveLaunchDelay = 0.3f;
        
        [SerializeField, Foldout("DEBUG")] private int _maxDiceCount = 50;
        [SerializeField, Foldout("DEBUG")] private int _waveLaunchCount = 5;
        private Queue<DiceBase> _diceQueue = new();

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        public void ClearDices()
        {
            while (_diceQueue.Count > 0)
            {
                Destroy(_diceQueue.Dequeue().gameObject);
            }
        }

        public DiceBase LaunchDice(DiceBase dicePrefab)
        {
            if (_diceQueue.Count > _maxDiceCount)
            {
                Destroy(_diceQueue.Dequeue().gameObject);
            }
            DiceBase dice = Instantiate(dicePrefab, transform.position, Random.rotation, transform);
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

            return dice;
        }

        private Dictionary<DiceBase, bool> _activeDiceMap = new();
        
        public async Awaitable<DiceBase> LaunchDieAndWaitForStop(DiceBase dicePrefab)
        {
            DiceBase launchDice = LaunchDice(dicePrefab);
            _activeDiceMap.Add(launchDice, true);
            launchDice.GetComponent<LaunchedDieBroadcaster>().StoppedMoving += OnStoppedMoving;
            while (_activeDiceMap[launchDice])
            {
                await Awaitable.EndOfFrameAsync();
            }
            _activeDiceMap.Remove(launchDice);
            return launchDice;

            void OnStoppedMoving(DiceBase obj)
            {
                _activeDiceMap[obj] = false;
            }
        }

        public async Awaitable<List<DiceBase>> LaunchDiceAndWaitForStop(List<DiceBase> prefabs)
        {
            List<Awaitable<DiceBase>> awaitables = new();
            foreach (DiceBase prefab in prefabs)
            {
                awaitables.Add(LaunchDieAndWaitForStop(prefab));
                await Awaitable.WaitForSecondsAsync(0.1f);
            }

            List<DiceBase> activeDiceList = new();
            foreach (Awaitable<DiceBase> awaitable in awaitables)
            {
                activeDiceList.Add(await awaitable);
            }
            return activeDiceList;
        }

        private void LaunchedDieBroadcasterOnStoppedMoving(DiceBase diceBase)
        {
            Debug.Log($"A Die has stopped moving, face value is: {diceBase.GetUpFaceValue()}");
        }

        private void OnDrawGizmos()
        {
            Color color = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * 5f);
            Gizmos.color = color;
        }
    }
}