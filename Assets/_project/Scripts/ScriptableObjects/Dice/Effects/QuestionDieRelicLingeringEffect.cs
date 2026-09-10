using System.Collections.Generic;
using System.Linq;
using _project.Scripts.ScriptableObjects.Relics;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Dice.Effects
{
    [CreateAssetMenu(fileName = "QuestionDieRelicLingeringEffect", menuName = "Chaos Yahtzee/Die Effects/Question Die Lingering Effect (Relic)")]
    public class QuestionDieRelicLingeringEffect : EffectBase
    {
        [SerializeField] private SerializedDictionary<RelicScriptableObjectBase, int> _relicDatas = new();
        [SerializeField] private bool _fizzleIfNoRelics = true;

            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int nonNullRelicCount = gamestate.Relics.Count(r => r != null);
            if ((nonNullRelicCount == 0 || gamestate.Relics.Count == 0) && _fizzleIfNoRelics) return; // If we have no relics fizzle

            int randIndex = Random.Range(0, nonNullRelicCount);
            RelicScriptableObjectBase targetRelic = gamestate.Relics[randIndex];

            List<RelicScriptableObjectBase> inter = new();
            foreach ((RelicScriptableObjectBase key, int value) in _relicDatas)
            {
                if (key == targetRelic) continue; 
                for (int i = 0; i < value; i++)
                {
                    inter.Add(key);
                }
            }
            
            if (inter.Count == 0) return;

            gamestate.Relics[randIndex] = inter[Random.Range(0, inter.Count)];
        }
    }
}