using System.Collections.Generic;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum State
{
    Menu, 
    Playing, 
    ShopDice, 
    ShopObject, 
    GameOver
};

namespace _project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private State _state;
        [SerializeField] private ScoreDataScriptableObject _scoreData;
        [SerializeField] private DiceLauncher _diceLauncher;
        private GameState _gameState;
        public int CurrentStage { get; private set; } = -1;

        [SerializeField] private List<DiceDataScriptableObject> _inventory;
        public List<DiceDataScriptableObject> Inventory { get { return _inventory; } set { _inventory = value; } }
        [SerializeField] private List<RelicScriptableObjectBase> _relics;
        public List<RelicScriptableObjectBase> Relics { get { return _relics; } set { _relics = value; } }

        [FormerlySerializedAs("GameStateResolved")] public UnityEvent RoundWon;
        public UnityEvent<GameState> RoundLost;
        public UnityEvent<int, int> ScoreUpdated;
        // private Awaitable _speedUpIfTimeExceededTask;
        // private CancellationTokenSource _speedUpIfTimeExceededCancellationTokenSource = new();

        public State CurrentState => _state;

        public ScoreDataScriptableObject ScoreData => _scoreData;


        public void ChangeStateToMenu()
        {
            ChangeState(State.Menu);
        }
        
        public void ChangeStateToGameOver()
        {
            ChangeState(State.GameOver);
        }
        
        public void ChangeStateToDiceShop()
        {
            ChangeState(State.ShopDice);
        }

        public void ChangeStateToRelicShop()
        {
            ChangeState(State.ShopObject);
        }
        
        public void ChangeStateToPlay()
        {
            ChangeState(State.Playing);
        }

        public void ChangeState(State newState)
        {
            _state = newState;
        }

        public void DoRound()
        {
            _ = DoRoundAsync();
        }

        public void NextTurn()
        {
            CurrentStage++;
        }

        [Button(enabledMode:EButtonEnableMode.Playmode)]
        public async Awaitable DoRoundAsync()
        {
            List<DiceBase> prefabs = new();
            foreach (DiceDataScriptableObject data in _inventory)
            {
                prefabs.Add(data.Prefab);
            }

            List<DiceBase> activeDices = await _diceLauncher.LaunchDiceAndWaitForStop(prefabs);
            
            _gameState = new GameState(CurrentStage, _diceLauncher, ScoreData, _inventory, _relics, activeDices);

            _gameState.GameStateResolved += GameStateOnGameStateResolved;
            _gameState.ScoreUpdated += OnScoreUpdated;
            _ = _gameState.Resolve();

            // _speedUpIfTimeExceededCancellationTokenSource = new CancellationTokenSource();
            _speedUpActivated = true;
            _speedUpTimer = 0;
            // _speedUpIfTimeExceededTask = SpeedUpIfTimeExceeded(6f, 2f);
        }

        private void OnScoreUpdated(int arg1, int arg2)
        {
            ScoreUpdated?.Invoke(arg1, arg2);
        }

        private bool _speedUpActivated = false;
        private float _speedUpTimer;
        
        private void Update()
        {
            if (_speedUpActivated)
            {
                _speedUpTimer += Time.deltaTime;
                if (_speedUpTimer >= 6f)
                {
                    Time.timeScale += 1;
                    Debug.Log($"GameManager::Update: Speeding up time to {Time.timeScale}");
        
                    _speedUpTimer -= 6f;
                }
            }
        }
        
        // Yeah so again Awaitables are  weird with cancellation so we'll be  using good old Update
        
        // private async Awaitable SpeedUpIfTimeExceeded(float delay, float newTimeScale)
        // {
        //     if (!_speedUpActivated) return;
        //     await Awaitable.WaitForSecondsAsync(delay);
        //     if (!_speedUpActivated) return;
        //     // if (cancellationToken.IsCancellationRequested) return;
        //     
        //     Debug.Log($"GameManager::SpeedUpIfTimeExceeded: Speeding up time to {newTimeScale}");
        //     Time.timeScale = newTimeScale;
        //
        //     if (newTimeScale >= 4) return;
        //     _speedUpIfTimeExceededTask = SpeedUpIfTimeExceeded(3f * Time.timeScale, Time.timeScale+1);
        // } 

        private void GameStateOnGameStateResolved(GameState gameState)
        {
            _speedUpActivated = false;
            // _speedUpIfTimeExceededTask?.Cancel();
            Debug.Log($"GameManager::GameStateOnGameStateResolved: setting time to 1");
            Time.timeScale = 1;
            
            if (gameState.CurrentRoundScore < gameState.TargetRoundScore)
            {
                Debug.LogWarning($"Game state was resolved: You lost (score: {gameState.CurrentRoundScore}/{gameState.TargetRoundScore})");
                OnGameOver(gameState);
                return;
            }
            
            Debug.Log($"Game state was resolved (score: {gameState.CurrentRoundScore}/{gameState.TargetRoundScore})");
            _ = DelayedGameStateResolved(3f);
        }

        private void OnGameOver(GameState gameState)
        {
            RoundLost?.Invoke(gameState);
        }

        public void StartRun()
        {
            CurrentStage = -1;
        }

        private async Awaitable DelayedGameStateResolved(float delayTime)
        {
            await Awaitable.WaitForSecondsAsync(delayTime);
            RoundWon?.Invoke();
        }


    }
}