using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class blackCloverDice : Dice
    {
        public new List<int> faces = new() { 20, 40, 80, 100, 140, 1 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 1)
            {

            }
        }
    }
}