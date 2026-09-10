using System;
using System.Collections.Generic;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        [HideInInspector] public List<DiceDataScriptableObject> Inventory { get { return _inventory; } set { _inventory = value; } }
        [SerializeField] private List<RelicScriptableObjectBase> _relics;
        [HideInInspector] public List<RelicScriptableObjectBase> Relics { get { return _relics; } set { _relics = value; } }

        public UnityEvent GameStateResolved;
        public UnityEvent<int, int> ScoreUpdated;

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

        void Start()
        {
            // _ = DoRoundAsync();
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
        }

        private void OnScoreUpdated(int arg1, int arg2)
        {
            ScoreUpdated?.Invoke(arg1, arg2);
        }

        private void GameStateOnGameStateResolved(GameState obj)
        {
            Debug.Log($"Game state was resolved (score: {obj.CurrentRoundScore})");
            _ = DelayedGameStateResolved(3f);
        }

        private async Awaitable DelayedGameStateResolved(float delayTime)
        {
            await Awaitable.WaitForSecondsAsync(delayTime);
            GameStateResolved?.Invoke();
        }


    }
}