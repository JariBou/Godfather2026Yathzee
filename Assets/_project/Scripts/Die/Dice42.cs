using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42 : Dice
    {
        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 42)
            {
                //gamestate.ThrowNewDice(new Dice42());
            }
        }
    }
}