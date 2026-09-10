using UnityEngine;

namespace _project.Scripts.ScriptableObjects.OtherEffects
{
    [CreateAssetMenu(fileName = "MultiplyScoreEffect", menuName = "Chaos Yahtzee/Effects/Multiply Score Effect")]
    public class MultiplyScoreEffect : EffectBase
    {
        [SerializeField] private float _multiplier;
        
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gameState)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            gameState.MultiplyScore(_multiplier);
        }

        public override string GetEffectDisplayIndicator()
        {
            return $"x {_multiplier}";
        }
    }
}