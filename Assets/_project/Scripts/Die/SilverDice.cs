using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class SilverDice : Dice
    {
        public new List<int> faces = new() { 5, 10, 15, 20, 25, 30 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
        }
    }
}