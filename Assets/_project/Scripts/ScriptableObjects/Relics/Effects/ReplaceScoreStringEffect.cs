using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics.Effects
{
    [CreateAssetMenu(fileName = "ReplaceScoreStringEffect", menuName = "Chaos Yahtzee/Relic Effects/Replace Score String Effect")]
    public class ReplaceScoreStringEffect : EffectBase
    {
        [SerializeField] private string _oldChar;
        [SerializeField] private string _newChar;
        
        
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gameState)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int score = gameState.CurrentRoundScore;
            string scoreString = score.ToString();
            scoreString = scoreString.Replace(_oldChar, _newChar);
            gameState.SetScore(int.Parse(scoreString));
        }
    }
}