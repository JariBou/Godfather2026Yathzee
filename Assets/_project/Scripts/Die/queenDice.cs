using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class queenDice : Dice
    {
        public new List<int> faces = new() { 20, 30, 40, 60, 1, 2 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 1)
            {

            }
            else if (faceScore == 0)
            {

            }
        }
    }
}