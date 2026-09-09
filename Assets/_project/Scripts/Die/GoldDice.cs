using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class GoldDice : Dice
    {
        public new List<int> faces = new() { 10, 20, 30, 40, 50, 60 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
        }
    }
}