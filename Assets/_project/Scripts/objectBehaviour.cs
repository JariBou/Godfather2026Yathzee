using UnityEngine;
using System.Collections.Generic;
using _project.Scripts.Die;
using UnityEditor;

public class objectBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

public abstract class Objects : MonoBehaviour
{
    public abstract float ApplyScoreEffect(Object gamestate, float score);
}

public abstract class MysticalBall : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        int random = UnityEngine.Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return score * 4;
            case 1:
                return score * 1;
            case 2:
                return score * .5f;
            case 3:
                return score * .25f;
            default : return score;
        }
    }
}


public abstract class cookie : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        int random = UnityEngine.Random.Range(0, 4);
        switch (random)
        {
            case 0:
                return score * 8;
            case 1:
                return score * 4;
            case 2:
                return score * 1;
            case 3:
                return score * 0.125f;
            default: return score;
        }
    }
}

public abstract class goatSkull : Objects
{

    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        string _score = score.ToString();
        int countOfSixs = _score.Length - _score.Replace("6", "").Length;
        if (countOfSixs == 3)
        {
            return score * 666;
        }
        else return score;
    }
}

public abstract class sheepSkull : Objects
{

    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        string _score = score.ToString();
        return float.Parse(_score.Replace("1", "9"));
    }

}   

public abstract class billyGoatSkull : Objects
{

    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        string _score = score.ToString();
        return float.Parse(_score.Replace("6", "9"));
    }
}

public abstract class horseShoe : Objects
{
    finalScore mult = GameObject.FindAnyObjectByType<finalScore>();
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        int random = UnityEngine.Random.Range(0, 5);
        switch (random)
        {
            case 0:
            case 1:
                return score * 4;
            case 2:
            case 3:
                return score * 1 /3f;
            case 4:
                return score * 6;

            default: return score;
        }
    }
}

public abstract class NoEntryCard : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        return score;
    }
}

public abstract class trophe42 : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        List<DiceBase> dinv = FindAnyObjectByType<inventoryBehaviour>().DiceInventory;
        for (int dice = 0; dice < dinv.Count; dice++)
        {
            if (dinv[dice] is Dice42)
            {
                FindAnyObjectByType<inventoryBehaviour>().replaceDice(new Dice42Trophy(), dice);
            }
        }
        //faut faire pareil pour la pool de dés
        return score;
    }
}

public abstract class cherry2 : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        for (int i = 0; i < score.ToString().Length - score.ToString().Replace("2", "").Length; i++)
        {
            score *= 2;
        }
        return score;
    }
}

public abstract class cherry3 : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        for (int i = 0; i < score.ToString().Length - score.ToString().Replace("3", "").Length; i++)
        {
            score *= 2;
        }
        return score;
    }
}

public abstract class cherry4 : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        for (int i = 0; i < score.ToString().Length - score.ToString().Replace("4", "").Length; i++)
        {
            score *= 2;
        }
        return score;
    }
}

public abstract class luckyQueen : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        inventoryBehaviour inv = FindAnyObjectByType<inventoryBehaviour>();

        for (int i = 0; i < inv.DiceInventory.Count; i++)
        {
            DiceBase diceBase = inv.DiceInventory[i];
            if (diceBase is GoldDice) inv.replaceDice(new EmeraldDice(), i);
            else if (diceBase is SilverDice) inv.replaceDice(new GoldDice(), i);
            else if (diceBase is BronzeDice) inv.replaceDice(new SilverDice(), i);
            else if (diceBase is BasicDice) inv.replaceDice(new BronzeDice(), i);
        }

        return score;
    }
}

public abstract class unLuckyQueen : Objects
{
    public override float ApplyScoreEffect(Object gamestate, float score)
    {
        inventoryBehaviour inv = FindAnyObjectByType<inventoryBehaviour>();

        for (int i = 0; i < inv.DiceInventory.Count; i++)
        {
            DiceBase diceBase = inv.DiceInventory[i];
            if (diceBase is EmeraldDice) inv.replaceDice(new GoldDice(), i);
            else if (diceBase is GoldDice) inv.replaceDice(new SilverDice(), i);
            else if (diceBase is SilverDice) inv.replaceDice(new BronzeDice(), i);
            else if (diceBase is BronzeDice) inv.replaceDice(new BasicDice(), i);
        }

        return score;
    }
}


