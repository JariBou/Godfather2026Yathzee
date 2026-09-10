using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42 : DiceBase
    {
    #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Awaitable ApplyEffect(GameState gamestate)
    #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            int upFaceValue = GetUpFaceValue();
            if (upFaceValue == 42)
            {
                gamestate.ActiveDices.Push(await gamestate.Launcher.LaunchDieAndWaitForStop(diceData.Prefab));
            }
            gamestate.AddScore(upFaceValue);
            Debug.Log("Applying Dice42 Effect");
        }
    }
}