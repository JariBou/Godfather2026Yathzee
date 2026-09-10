using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics
{
    [CreateAssetMenu(fileName = "SimpleEffectRelic", menuName = "Chaos Yahtzee/Relics/Simple Effect Relic")]
    public  class SimpleEffectRelic : RelicScriptableObjectBase
    {
        [SerializeField] private EffectBase _effect;
    
        public override void ApplyEffect(GameState gamestate)
        {
            gamestate.AddEffect(_effect);
        }
    }
}