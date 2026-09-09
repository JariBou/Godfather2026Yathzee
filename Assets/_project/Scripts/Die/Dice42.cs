using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42 : Dice
    {
        public new List<int> faces = new() { 1, 2, 3, 4, 42, 42 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 42)
            {
                //gamestate.ThrowNewDice(new Dice42());
            }
        }
    }
}