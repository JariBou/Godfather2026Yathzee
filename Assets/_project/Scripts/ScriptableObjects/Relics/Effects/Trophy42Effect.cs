using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics.Effects
{
    [CreateAssetMenu(fileName = "Trophy42Effect", menuName = "Chaos Yahtzee/Relic Effects/Trophy 42 Lingering Effect")]
    public class Trophy42Effect : EffectBase
    {
        [SerializeField] private DiceDataScriptableObject _base42DiceData;
        [SerializeField] private DiceDataScriptableObject _superDiceData;
            
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            for (int i = 0; i < gamestate.Inventory.Count; i++)
            {
                if (gamestate.Inventory[i] == _base42DiceData)
                {
                    gamestate.Inventory[i] = _superDiceData;
                }
            }
        }
    }
}