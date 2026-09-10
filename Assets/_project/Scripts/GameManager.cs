using System;
using System.Collections.Generic;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

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

        [SerializeField] private List<DiceDataScriptableObject> _inventory;
        [HideInInspector] public List<DiceDataScriptableObject> Inventory { get { return _inventory; } set { _inventory = value; } }
        [SerializeField] private List<RelicScriptableObjectBase> _relics;
        [HideInInspector] public List<RelicScriptableObjectBase> Relics { get { return _relics; } set { _relics = value; } }

        public UnityEvent GameStateResolved;

        public State CurrentState => _state;
        
        
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

        private void Start()
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

        [Button(enabledMode:EButtonEnableMode.Playmode)]
        public async Awaitable DoRoundAsync()
        {
            List<DiceBase> prefabs = new();
            foreach (DiceDataScriptableObject data in _inventory)
            {
                prefabs.Add(data.Prefab);
            }

            List<DiceBase> activeDices = await _diceLauncher.LaunchDiceAndWaitForStop(prefabs);
            
            _gameState = new GameState(_diceLauncher, _scoreData, _inventory, _relics, activeDices);

            _gameState.GameStateResolved += GameStateOnGameStateResolved;
            _ = _gameState.Resolve();
        }

        private void GameStateOnGameStateResolved(GameState obj)
        {
            Debug.Log($"Game state was resolved (score: {obj.CurrentRoundScore})");
            GameStateResolved?.Invoke();
        }
    }
}