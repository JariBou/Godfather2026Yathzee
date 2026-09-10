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
        private Dictionary<EffectPriority, Stack<EffectBase>> Effects { get; set; } = new();

        public List<DiceDataScriptableObject> Inventory { get; private set; }
        
        public event Action<GameState> GameStateResolved;


        public GameState(DiceLauncher diceLauncher, ScoreDataScriptableObject scoreData, List<DiceDataScriptableObject> inventory,
                         List<DiceBase> activeDices)
        {
            Inventory = inventory;
            ScoreData = scoreData;
            Launcher = diceLauncher;
            ActiveDices = new Stack<DiceBase>(activeDices);
        }

        public void AddScore(int value)
        {
            CurrentRoundScore += value;
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
            while (ActiveDices.Count > 0)
            {
                DiceBase dice = ActiveDices.Pop();
                await dice.ApplyEffect(this);
            }

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
                    }
                }
            }

            await Awaitable.MainThreadAsync();
            GameStateResolved?.Invoke(this);
        }
    }
}