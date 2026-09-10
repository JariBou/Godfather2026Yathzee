using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Dice.Effects
{
    [CreateAssetMenu(fileName = "HealerDieLingeringEffect", menuName = "Chaos Yahtzee/Die Effects/Healer Die Lingering Effect")]
    public class HealerDieLingeringEffect : EffectBase
    {
        [SerializeField] private DiceDataScriptableObject _diceData;
            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            for (int index = 0; index < gamestate.Inventory.Count; index++)
            {
                DiceDataScriptableObject data = gamestate.Inventory[index];
                if (data.Prefab is CursedDice)
                {
                    gamestate.Inventory[index] = _diceData;
                }
            }
        }
    }
}