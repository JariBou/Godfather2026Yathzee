using UnityEngine;
using System.Collections.Generic;

public class diceBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public inventoryBehaviour inventory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}

public abstract class Dice : MonoBehaviour
{
    public List<int> faces = new() { 1, 2, 3, 4, 5, 6 };

    public abstract void ApplyEffect(int faceScore, Object gamestate);
}

public class Dice42 : Dice
{
    public new List<int> faces = new() { 1, 2, 3, 4, 42, 42 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
        if (faceScore == 42)
        {
            //gamestate.ThrowNewDice(new Dice42());
        }
    }
}

public class Dice42Trophy : Dice
{
    public new List<int> faces = new() { 1, 1, 42, 42, 42, 42 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
        if (faceScore == 42)
        {
            //gamestate.ThrowNewDice(new Dice42Trophy());
        }
    }
}

public class BasicDice : Dice
{
    public new List<int> faces = new() { 1, 2, 3, 4, 5, 6 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
    }
}

public class BronzeDice : Dice
{
    public new List<int> faces = new() { 2, 4, 6, 8, 10, 12 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
    }
}

public class SilverDice : Dice
{
    public new List<int> faces = new() { 5, 10, 15, 20, 25, 30 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
    }
}

public class GoldDice : Dice
{
    public new List<int> faces = new() { 10, 20, 30, 40, 50, 60 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
    }
}
public class EmeraldDice : Dice
{
    public new List<int> faces = new() { 100, 200, 300, 400, 500, 600 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
    }
}

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

public class blackCloverDice : Dice
{
    public new List<int> faces = new() { 20, 40, 80, 100, 140, 1 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
        if (faceScore == 1)
        {

        }
    }
}

public class goldCloverDice : Dice
{
    public new List<int> faces = new() { 10, 20, 30, 40, 50, 1 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
        if (faceScore == 1)
        {

        }
    }
}

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

public class queenDice : Dice
{
    public new List<int> faces = new() { 20, 30, 40, 60, 1, 2 };


    public override void ApplyEffect(int faceScore, Object gamestate)
    {
        if (faceScore == 1)
        {

        }
        else if (faceScore == 0)
        {

        }
    }
}

