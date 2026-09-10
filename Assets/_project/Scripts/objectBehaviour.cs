using UnityEngine;

namespace _project.Scripts
{
    public abstract class RelicScriptableObjectBase : ScriptableObject
    {
        public abstract void ApplyEffect(GameState gamestate);
    }

    public class MysticalBall : RelicScriptableObjectBase
    {
        public override void ApplyEffect(GameState gamestate)
        {
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    gamestate.MultiplyScore(4);
                    break;
                case 1:
                    gamestate.MultiplyScore(1);
                    break;
                case 2:
                    gamestate.MultiplyScore(.5f);
                    break;
                case 3:
                    gamestate.MultiplyScore(.25f);
                    break;
            }
        }
    }


    public  class cookie : RelicScriptableObjectBase
    {
        public override void ApplyEffect(GameState gamestate)
        {
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    gamestate.MultiplyScore(8);
                    break;
                case 1:
                    gamestate.MultiplyScore(4);
                    break;
                case 2:
                    gamestate.MultiplyScore(1);
                    break;
                case 3:
                    gamestate.MultiplyScore(.125f);
                    break;
            }
        }
    }

    public  class goatSkull : RelicScriptableObjectBase
    {

        public override void ApplyEffect(GameState gamestate)
        {
            int score = gamestate.CurrentRoundScore;
            string scoreString = score.ToString();
            int countOfSixs = scoreString.Length - scoreString.Replace("6", "").Length;
            if (countOfSixs == 3)
            {
                gamestate.MultiplyScore(666f);
            }
        }
    }

    public  class sheepSkull : RelicScriptableObjectBase
    {

        public override void ApplyEffect(GameState gamestate)
        {
            int score = gamestate.CurrentRoundScore;
        
            string scoreString = score.ToString();
            gamestate.SetScore(float.Parse(scoreString.Replace("1", "9")));
        }

    }   

    public  class billyGoatSkull : RelicScriptableObjectBase
    {

        public override void ApplyEffect(GameState gamestate)
        {
            int score = gamestate.CurrentRoundScore;
        
            string scoreString = score.ToString();
            gamestate.SetScore(float.Parse(scoreString.Replace("6", "9")));
        }
    }

    public  class horseShoe : RelicScriptableObjectBase
    {
        finalScore mult = FindAnyObjectByType<finalScore>();
        public override void ApplyEffect(GameState gamestate)
        {
            int random = Random.Range(0, 5);
            switch (random)
            {
                case 0:
                case 1:
                    gamestate.MultiplyScore(4);
                    break;
                case 2:
                case 3:
                    gamestate.MultiplyScore(1/3f);
                    break;
                case 4:
                    gamestate.MultiplyScore(6);
                    break;
            }
        }
    }

    public  class NoEntryCard : RelicScriptableObjectBase
    {
        public override void ApplyEffect(GameState gamestate)
        {
            //TODO
        }
    }

    // public  class cherry2 : Objects
    // {
    //     public override void ApplyEffect(GameState gamestate)
    //     {
    //         for (int i = 0; i < score.ToString().Length - score.ToString().Replace("2", "").Length; i++)
    //         {
    //             score *= 2;
    //         }
    //         return score;
    //     }
    // }
    //
    // public  class cherry3 : Objects
    // {
    //     public override void ApplyEffect(GameState gamestate)
    //     {
    //         for (int i = 0; i < score.ToString().Length - score.ToString().Replace("3", "").Length; i++)
    //         {
    //             score *= 2;
    //         }
    //         return score;
    //     }
    // }
    //
    // public  class cherry4 : Objects
    // {
    //     public override void ApplyEffect(GameState gamestate)
    //     {
    //         for (int i = 0; i < score.ToString().Length - score.ToString().Replace("4", "").Length; i++)
    //         {
    //             score *= 2;
    //         }
    //         return score;
    //     }
    // }
    //
    // public  class luckyQueen : Objects
    // {
    //     public override void ApplyEffect(GameState gamestate)
    //     {
    //         inventoryBehaviour inv = FindAnyObjectByType<inventoryBehaviour>();
    //
    //         for (int i = 0; i < inv.DiceInventory.Count; i++)
    //         {
    //             DiceBase diceBase = inv.DiceInventory[i];
    //             if (diceBase is GoldDice) inv.replaceDice(new EmeraldDice(), i);
    //             else if (diceBase is SilverDice) inv.replaceDice(new GoldDice(), i);
    //             else if (diceBase is BronzeDice) inv.replaceDice(new SilverDice(), i);
    //             else if (diceBase is BasicDice) inv.replaceDice(new BronzeDice(), i);
    //         }
    //
    //         return score;
    //     }
    // }
    //
    // public  class unLuckyQueen : Objects
    // {
    //     public override void ApplyEffect(GameState gamestate)
    //     {
    //         inventoryBehaviour inv = FindAnyObjectByType<inventoryBehaviour>();
    //
    //         for (int i = 0; i < inv.DiceInventory.Count; i++)
    //         {
    //             DiceBase diceBase = inv.DiceInventory[i];
    //             if (diceBase is EmeraldDice) inv.replaceDice(new GoldDice(), i);
    //             else if (diceBase is GoldDice) inv.replaceDice(new SilverDice(), i);
    //             else if (diceBase is SilverDice) inv.replaceDice(new BronzeDice(), i);
    //             else if (diceBase is BronzeDice) inv.replaceDice(new BasicDice(), i);
    //         }
    //
    //         return score;
    //     }
    // }
}