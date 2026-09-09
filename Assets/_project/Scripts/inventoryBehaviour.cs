using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using _project.Scripts.Die;

public class inventoryBehaviour : MonoBehaviour
{
    public List<DiceBase> DiceInventory = new List<DiceBase>();
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

    public void addDice (DiceBase diceBase)
    {
        DiceInventory.Add(diceBase);
    }

    public void replaceDice(DiceBase diceBase, int index) 
    {
        DiceInventory[index] = diceBase;
    }
}
