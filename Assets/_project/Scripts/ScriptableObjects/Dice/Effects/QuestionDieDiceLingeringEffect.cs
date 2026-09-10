using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Dice.Effects
{
    [CreateAssetMenu(fileName = "QuestionDieDiceLingeringEffect", menuName = "Chaos Yahtzee/Die Effects/Question Die Lingering Effect (Dice)")]
    public class QuestionDieDiceLingeringEffect : EffectBase
    {
        [SerializeField] private SerializedDictionary<DiceDataScriptableObject, int> _diceDatas = new();
            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int randIndex = Random.Range(0, gamestate.Inventory.Count);
            DiceDataScriptableObject targetDice = gamestate.Inventory[randIndex];

            List<DiceDataScriptableObject> inter = new();
            foreach ((DiceDataScriptableObject key, int value) in _diceDatas)
            {
                if (key == targetDice) continue;
                
                for (int i = 0; i < value; i++)
                {
                    inter.Add(key);
                }
            }
            
            if (inter.Count == 0) return;

            gamestate.Inventory[randIndex] = inter[Random.Range(0, inter.Count)];
        }
    }
}