using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class Dice42Trophy : Dice
    {
        public new List<int> faces = new() { 1, 1, 42, 42, 42, 42 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 42)
            {
                //gamestate.ThrowNewDice(new Dice42Trophy());
            }
        }
    }
}