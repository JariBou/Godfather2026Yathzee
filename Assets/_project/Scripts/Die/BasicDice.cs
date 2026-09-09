using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class BasicDice : Dice
    {
        public new List<int> faces = new() { 1, 2, 3, 4, 5, 6 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
        }
    }
}