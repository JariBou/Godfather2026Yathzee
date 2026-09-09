using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class CursedDice : Dice
    {
        public new List<int> faces = new() { 1, 1, 1, 1, 1, 1000 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
            if (faceScore == 1)
            {
            
            }
        }
    }
}