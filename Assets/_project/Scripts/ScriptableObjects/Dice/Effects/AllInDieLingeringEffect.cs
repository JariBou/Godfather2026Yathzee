using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Dice.Effects
{
    [CreateAssetMenu(fileName = "AllInDieLingeringEffect", menuName = "Chaos Yahtzee/Die Effects/All In Die Lingering Effect")]
    public class AllInDieLingeringEffect : EffectBase
    {
        [SerializeField] private DiceDataScriptableObject _diceData;
            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            for (int index = 0; index < gamestate.Inventory.Count; index++)
            {
                DiceDataScriptableObject data = gamestate.Inventory[index];
                if (data != _diceData)
                {
                    gamestate.Inventory[index] = _diceData;
                }
            }
        }
    }
}