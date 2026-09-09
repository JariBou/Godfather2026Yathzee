using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class EmeraldDice : Dice
    {
        public new List<int> faces = new() { 100, 200, 300, 400, 500, 600 };


        public override void ApplyEffect(int faceScore, Object gamestate)
        {
        }
    }
}