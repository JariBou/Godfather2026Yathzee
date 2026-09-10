using _project.Scripts.Die;
using _project.Scripts.ScriptableObjects;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts
{
    public class ShopManager : MonoBehaviour
    {
        [FormerlySerializedAs("itemPool"), SerializeField] private SerializedDictionary<RelicScriptableObjectBase, int> _itemPool = new();
        private readonly List<RelicScriptableObjectBase> _shopItemPool = new();

        public SerializedDictionary<DiceDataScriptableObject, int> dicePool = new();
        private readonly List<DiceDataScriptableObject> _shopDicePool = new();

        [SerializeField] private int _proposedDiceCount = 3;
        [SerializeField] private int _proposedRelicCount = 1;
        
        [SerializeField] private GameManager _gameManager;

        private int _shopIndex = -1;
        private int _invIndex = -1;
    

        public void RefreshShop()
        {
            List<RelicScriptableObjectBase> randItemPool = new();
            _shopItemPool.Clear();
            foreach ((RelicScriptableObjectBase item, int quantity) in _itemPool) 
            {
                for (int i = 0; i < quantity; i++)
                {
                    randItemPool.Add(item);
                }
            }
            randItemPool = randItemPool.OrderBy(x=>Random.value).ToList();
            for (int i = 0; i < _proposedRelicCount; i++)
            { 
                int randomItem = Random.Range(0, randItemPool.Count);
                RelicScriptableObjectBase randItem = randItemPool[randomItem];
                _itemPool[randItem] -= 1;
                _shopItemPool.Add(randItem);
            }
        

            
            List<DiceDataScriptableObject> rantDicePool = new();
            _shopDicePool.Clear();
            foreach ((DiceDataScriptableObject item, int quantity) in dicePool)
            {
                for (int i = 0; i < quantity; i++)
                {
                    rantDicePool.Add(item);
                }
            }
            rantDicePool = rantDicePool.OrderBy(x => Random.value).ToList();
            for (int i = 0; i < _proposedDiceCount; i++)
            {
                int randomItem = Random.Range(0, rantDicePool.Count);
                DiceDataScriptableObject randItem = rantDicePool[randomItem];
                dicePool[randItem] -= 1;
                _shopDicePool.Add(randItem);
            }

        }

        public void CloseShop()
        {
            foreach (RelicScriptableObjectBase relic in _shopItemPool)
            {
                _itemPool[relic] += 1;
            }

            foreach (DiceDataScriptableObject diceBase in _shopDicePool)
            {
                dicePool[diceBase] += 1;
            }
        }

        public void TakeDice(int shopIndex, int invIndex)
        {
            (_shopDicePool[shopIndex], _gameManager.Inventory[invIndex]) = (_gameManager.Inventory[invIndex], _shopDicePool[shopIndex]);
            
            Debug.Log("Dés" + shopIndex + invIndex);

        }

        public void TakeItem(int shopIndex, int invIndex)
        {
            (_shopItemPool[shopIndex], _gameManager.Relics[invIndex]) = (_gameManager.Relics[invIndex], _shopItemPool[shopIndex]);
            Debug.Log("Item"+shopIndex + invIndex);
        }

        public void ChooseShop(int index)
        {
            _shopIndex = index;

            if (_shopIndex!= -1 && _invIndex != -1)
            {
                if (FindAnyObjectByType<GameManager>().CurrentState == State.ShopDice)
                {
                    TakeDice(_shopIndex, _invIndex);
                }
                else if (FindAnyObjectByType<GameManager>().CurrentState == State.ShopObject)
                {
                    TakeItem(_shopIndex, _invIndex);
                }


                (_shopIndex, _invIndex) = (-1, -1);
            }
        }
        public void ChooseInv(int index)
        {
            _invIndex = index;

            if (_shopIndex != -1 && _invIndex != -1)
            {
                if (FindAnyObjectByType<GameManager>().CurrentState == State.ShopDice)
                {
                    TakeDice(_shopIndex, _invIndex);
                }
                else if (FindAnyObjectByType<GameManager>().CurrentState == State.ShopObject)
                {
                    TakeItem(_shopIndex, _invIndex);
                }


                (_shopIndex, _invIndex) = (-1, -1);
            }
        }

    }
}
