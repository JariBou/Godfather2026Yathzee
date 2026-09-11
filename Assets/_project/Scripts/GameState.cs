using System;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts.Die;
using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using UnityEngine;

namespace _project.Scripts
{
    public class GameState
    {
        private const float EffectApplicationDelay = 0.1f;
        public DiceLauncher Launcher { get; private set; }
        public ScoreDataScriptableObject ScoreData { get; private set; }
        public int CurrentRoundScore { get; private set; }
        public int TargetRoundScore { get; private set; }
        public Stack<DiceBase> ActiveDice { get; private set; }
        public List<DiceBase> InGameDice { get; private set; } = new();
        private Dictionary<EffectPriority, Stack<EffectBase>> Effects { get; set; } = new();
        public List<DiceDataScriptableObject> Inventory { get; private set; }
        public List<RelicScriptableObjectBase> Relics { get; set; }

        public int CurrentStage { get; private set; }

        public event Action<GameState> GameStateResolved;
        public event Action<int, int> ScoreUpdated;


        public GameState(int currentStage, DiceLauncher diceLauncher, ScoreDataScriptableObject scoreData, List<DiceDataScriptableObject> inventory,
                         List<RelicScriptableObjectBase> relics,
                         List<DiceBase> activeDices)
        {
            CurrentStage = currentStage;
            TargetRoundScore = scoreData.GetTargetScoreForStageInt(CurrentStage);
            Relics = relics;
            Inventory = inventory;
            ScoreData = scoreData;
            Launcher = diceLauncher;
            ActiveDice = new Stack<DiceBase>(activeDices);
        }

        public void AddScore(int value)
        {
            CurrentRoundScore += value;
            ScoreUpdated?.Invoke(CurrentRoundScore, TargetRoundScore);
        }

        public void MultiplyScore(float value)
        {
            CurrentRoundScore = Mathf.CeilToInt(CurrentRoundScore * value);
            ScoreUpdated?.Invoke(CurrentRoundScore, TargetRoundScore);
        }

        public void SetScore(int value)
        {
            CurrentRoundScore = value;
            ScoreUpdated?.Invoke(CurrentRoundScore, TargetRoundScore);
        }
        
        public void SetScore(float value)
        {
            CurrentRoundScore = Mathf.CeilToInt(value);
            ScoreUpdated?.Invoke(CurrentRoundScore, TargetRoundScore);
        }

        public void AddEffect(EffectBase effect)
        {
            Debug.Log($"Adding effect '{effect.GetType().Name}'");
            if (Effects.TryGetValue(effect.Priority, out Stack<EffectBase> value))
            {
                value.Push(effect);
            }
            else
            {
                Effects.Add(effect.Priority, new Stack<EffectBase>(new[] { effect }));
            }
        }

        public async Awaitable Resolve()
        {
            foreach (RelicScriptableObjectBase relic in Relics.Where(relic => relic != null))
            {
                relic.ApplyEffect(this);
            }

            await ResolveDice();

        #pragma warning disable CS0612 // Type or member is obsolete
            for (int i = 0; i < (int)EffectPriority.COUNT; i++)
        #pragma warning restore CS0612 // Type or member is obsolete
            {
                if (Effects.TryGetValue((EffectPriority)i, out Stack<EffectBase> effectStack))
                {
                    while (effectStack.Count > 0)
                    {
                        EffectBase effect = effectStack.Pop();
                        await effect.ApplyEffect(this);
                        await Awaitable.WaitForSecondsAsync(EffectApplicationDelay);
                    }
                }
            }
            
            await Awaitable.WaitForSecondsAsync(EffectApplicationDelay * 3f);

            await Awaitable.MainThreadAsync();
            GameStateResolved?.Invoke(this);
        }

        public async Awaitable ResolveDice()
        {
            while (ActiveDice.Count > 0)
            {
                DiceBase dice = ActiveDice.Pop();
                InGameDice.Add(dice);
                await dice.ApplyEffect(this);
                await Awaitable.WaitForSecondsAsync(EffectApplicationDelay);
            }
        }
    }
}