using System;
using System.Collections.Generic;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using NaughtyAttributes;
using UnityEngine;

namespace _project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreDataScriptableObject _scoreData;
        [SerializeField] private DiceLauncher _diceLauncher;
        private GameState _gameState;

        [SerializeField] private List<DiceDataScriptableObject> _inventory;
        [SerializeField] private List<RelicScriptableObjectBase> _relics;

        public void DoRound()
        {
            _diceLauncher.LaunchDice();
        }

        private void Start()
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
        }
    }
}