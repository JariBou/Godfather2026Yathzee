using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics.Effects
{
    [CreateAssetMenu(fileName = "RandomEffectsEffect", menuName = "Chaos Yahtzee/Relic Effects/Random Outcome Effect")]
    public class RandomEffectsEffect : EffectBase
    {
        [SerializeField] private SerializedDictionary<string, EffectBase> _possibleOutcomes = new();
        
        
        public override async Awaitable ApplyEffect(GameState gameState)
        {
            if (_possibleOutcomes.Count == 0) return;

            int outcomeIndex = Random.Range(0, _possibleOutcomes.Count);
            string outcomesKey = _possibleOutcomes.Keys.ToList()[outcomeIndex];

            EffectBase possibleOutcome = _possibleOutcomes[outcomesKey];
            if (possibleOutcome == null) return;
            
            await possibleOutcome.ApplyEffect(gameState);
        }
    }
}