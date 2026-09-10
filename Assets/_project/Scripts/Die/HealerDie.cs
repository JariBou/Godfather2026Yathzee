using _project.Scripts.ScriptableObjects.Dice.Effects;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class HealerDie : DiceBase
    {
        [SerializeField] protected HealerDieLingeringEffect effect;
        
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int upFaceValue = GetUpFaceValue();
            gamestate.AddScore(upFaceValue);
            if (upFaceValue == 1)
            {
                gamestate.AddEffect(effect);
            }
            SpawnScoreEffect(upFaceValue);
        }
    }
}