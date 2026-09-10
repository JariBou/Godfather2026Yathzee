using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42 : DiceBase
    {
        public override async Awaitable ApplyEffect(GameState gamestate)
        {
            int upFaceValue = GetUpFaceValue();
            SpawnScoreEffect(upFaceValue);
            gamestate.AddScore(upFaceValue);
            if (upFaceValue == 42)
            {
                gamestate.ActiveDice.Push(await gamestate.Launcher.LaunchDieAndWaitForStop(diceData.Prefab));
            }
            Debug.Log("Applying Dice42 Effect");
        }
    }
}