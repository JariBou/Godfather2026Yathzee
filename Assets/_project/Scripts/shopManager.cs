using _project.Scripts.Die;
using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts;
using TMPro;
using UnityEngine;

using static UnityEditor.Progress;

public class shopManager : MonoBehaviour
{
    public SerializedDictionary<RelicScriptableObjectBase, int> itemPool = new SerializedDictionary<RelicScriptableObjectBase, int>();
    [HideInInspector] public List<RelicScriptableObjectBase> _itemPool = new List<RelicScriptableObjectBase>();
    [HideInInspector] public List<RelicScriptableObjectBase> shopItemPool = new List<RelicScriptableObjectBase>();
    [Header("")] 
    public SerializedDictionary<DiceBase, int> dicePool = new SerializedDictionary<DiceBase, int>();
    [HideInInspector]public List<DiceBase> _dicePool = new List<DiceBase>();
    [HideInInspector] public List<DiceBase> shopDicePool = new List<DiceBase>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshShop()
    {
        _itemPool.Clear();
        shopItemPool.Clear();
        foreach (var pair in itemPool) 
        {
            RelicScriptableObjectBase _item = pair.Key;
            int quantity = pair.Value;
            for (int i = 0; i < quantity; i++)
            {
                _itemPool.Add(_item);
            }
        }
        _itemPool = _itemPool.OrderBy(x=>Random.value).ToList();
        for (int i = 0; i < 3; i++)
        { 
            int randomitem = Random.Range(0, _itemPool.Count);
            RelicScriptableObjectBase __item = _itemPool[randomitem];
            itemPool[__item] = itemPool[__item] - 1;
            shopItemPool.Add(__item);
        }



        _dicePool.Clear();
        shopDicePool.Clear();
        foreach (var pair in dicePool)
        {
            DiceBase _item = pair.Key;
            int quantity = pair.Value;
            for (int i = 0; i < quantity; i++)
            {
                _dicePool.Add(_item);
            }
        }
        _dicePool = _dicePool.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < 3; i++)
        {
            int randomitem = Random.Range(0, _dicePool.Count);
            DiceBase __item = _dicePool[randomitem];
            dicePool[__item] = dicePool[__item] - 1;
            shopDicePool.Add(__item);
        }

    }

    public void closeShop()
    {
        for (int i=0; i<shopItemPool.Count; i++)
        {
            itemPool[shopItemPool[i]] = itemPool[shopItemPool[i]] + 1;
        }


        for (int i = 0; i < shopDicePool.Count; i++)
        {
            dicePool[shopDicePool[i]] = dicePool[shopDicePool[i]] + 1;
        }

    }

}
