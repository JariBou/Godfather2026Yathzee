using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class redCloverDice : Dice
    {
        public new List<int> faces = new() { 12, 22, 32, 42 , 52, 1 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 1)
            {

            }
        }
    }
}