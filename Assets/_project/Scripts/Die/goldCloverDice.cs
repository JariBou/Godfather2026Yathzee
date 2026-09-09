using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class goldCloverDice : Dice
    {
        public new List<int> faces = new() { 10, 20, 30, 40, 50, 1 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 1)
            {

            }
        }
    }
}