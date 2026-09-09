using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class BronzeDice : Dice
    {
        public new List<int> faces = new() { 2, 4, 6, 8, 10, 12 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
        }
    }
}