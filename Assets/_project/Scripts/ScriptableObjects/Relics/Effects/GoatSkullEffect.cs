using _project.Scripts.Effects.UIScore;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics.Effects
{
    [CreateAssetMenu(fileName = "GoatSkullEffect", menuName = "Chaos Yahtzee/Relic Effects/Goat Skull Effect")]
    public class GoatSkullEffect : EffectBase
    {
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gameState)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int score = gameState.CurrentRoundScore;
            string scoreString = score.ToString();
            int countOfSixs = scoreString.Length - scoreString.Replace("6", "").Length;
            if (countOfSixs == 3)
            {
                ScoreMultiplierVfxManager vfxManager = FindFirstObjectByType<ScoreMultiplierVfxManager>();
                vfxManager.ShowVfx("x 666");
                gameState.MultiplyScore(666f);
            }
        }
    }
}