using _project.Scripts.ScriptableObjects.Relics.Effects;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics
{
    [CreateAssetMenu(fileName = "RandomOutcomeRelic", menuName = "Chaos Yahtzee/Relics/Random Outcome Relic")]
    public class RandomOutcomeRelic : RelicScriptableObjectBase
    {
        [SerializeField] private RandomEffectsEffect _effect;
    
        public override void ApplyEffect(GameState gamestate)
        {
            gamestate.AddEffect(_effect);
        }
    }
}