using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics
{
    [CreateAssetMenu(fileName = "Trophy42", menuName = "Chaos Yahtzee/Relics/Trophy 42")]
    public  class Trophy42 : RelicScriptableObjectBase
    {
        [SerializeField] private EffectBase _effect;
    
        public override void ApplyEffect(GameState gamestate)
        {
            gamestate.AddEffect(_effect);
        }
    }
}