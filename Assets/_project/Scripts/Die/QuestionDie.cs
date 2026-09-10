using _project.Scripts.ScriptableObjects.Dice.Effects;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class QuestionDie : DiceBase
    {
        [SerializeField] private QuestionDieDiceLingeringEffect _diceEffect;
        [SerializeField] private QuestionDieRelicLingeringEffect _relicEffect;
        
        
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int upFaceValue = GetUpFaceValue();

            switch (upFaceValue)
            {
                case -1:
                    SpawnScoreEffect(50);
                    gamestate.AddEffect(_diceEffect);
                    break;
                case -2:
                    SpawnScoreEffect(50);
                    gamestate.AddEffect(_relicEffect);
                    break;
                default:
                    SpawnScoreEffect(upFaceValue);
                    gamestate.AddScore(upFaceValue);
                    break;
            }
        }
    }
}