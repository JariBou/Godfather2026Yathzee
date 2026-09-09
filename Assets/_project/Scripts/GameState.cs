using System;
using System.Collections.Generic;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _project.Scripts
{
    public class GameState
    {
        public DiceLauncher Launcher { get; private set; }
        public ScoreDataScriptableObject ScoreData { get; private set; }
        public int CurrentRoundScore { get; private set; }
        public Stack<DiceBase> ActiveDices { get; private set; }
        
        public event Action<GameState> GameStateResolved;


        public GameState(DiceLauncher diceLauncher, ScoreDataScriptableObject scoreData, List<DiceBase> activeDices)
        {
            ScoreData = scoreData;
            Launcher = diceLauncher;
            ActiveDices = new Stack<DiceBase>(activeDices);
        }

        public void AddScore(int value)
        {
            CurrentRoundScore += value;
        }

        public async Awaitable Resolve()
        {
            while (ActiveDices.Count > 0)
            {
                DiceBase dice = ActiveDices.Pop();
                await dice.ApplyEffect(this);
            }

            await Awaitable.MainThreadAsync();
            GameStateResolved?.Invoke(this);
        }
    }
}