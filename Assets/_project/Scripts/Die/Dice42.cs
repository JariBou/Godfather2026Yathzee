using System;
using System.Threading.Tasks;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42 : DiceBase
    {
        public override async Awaitable ApplyEffect(GameState gamestate)
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