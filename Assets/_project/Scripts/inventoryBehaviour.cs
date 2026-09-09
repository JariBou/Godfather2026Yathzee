using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using _project.Scripts.Die;

public class inventoryBehaviour : MonoBehaviour
{
    public List<Dice> DiceInventory = new List<Dice>();
    public List<Objects> ObjectsInventory = new List<Objects>();


    void Start()
    {
        for (int i = 0; i < 5; i++) 
        { 
            DiceInventory.Add(new BasicDice());
        }

    }


    void Update()
    {
        
    }

    public void addDice (Dice dice)
    {
        DiceInventory.Add(dice);
    }

    public void replaceDice(Dice dice, int index) 
    {
        DiceInventory[index] = dice;
    }
}
