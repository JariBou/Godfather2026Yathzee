using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Dice.Effects
{
    [CreateAssetMenu(fileName = "CursedDiceLingeringEffect", menuName = "Chaos Yahtzee/Die Effects/Cursed Dice Lingering Effect")]
    public class CursedDiceLingeringEffect : EffectBase
    {
        [SerializeField] private DiceDataScriptableObject _diceData;
            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int randIndex = Random.Range(0, gamestate.Inventory.Count);
            DiceDataScriptableObject diceDataScriptableObject = gamestate.Inventory[randIndex];
            if (diceDataScriptableObject.Prefab is CursedDice)
            {
                    
            }
            else
            {
                gamestate.Inventory[randIndex] = _diceData;
            }
        }
    }
}