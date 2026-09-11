using System.Collections.Generic;
using System.Linq;
using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics.Effects
{
    [CreateAssetMenu(fileName = "ReplayEffect", menuName = "Chaos Yahtzee/Relic Effects/Replay Effect")]
    public class ReplayEffect : EffectBase
    {
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gameState)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            List<DiceBase> prefabs = gameState.Inventory.Select(data => data.Prefab).ToList();

            await gameState.Launcher.LaunchDiceAndWaitForStop(prefabs);
        }
    }
}