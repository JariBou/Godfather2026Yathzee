using System.Collections.Generic;
using UnityEngine;

namespace _project.Scripts.Die
{
    public class blackCloverDice : DiceBase
    {
        public new List<int> faces = new() { 20, 40, 80, 100, 140, 1 };


        public override Awaitable ApplyEffect(GameState gamestate)
        {
            return null;
        }
    }
}