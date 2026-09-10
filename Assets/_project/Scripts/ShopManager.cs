using System.Collections.Generic;
using System.Linq;
using _project.Scripts.Die;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts
{
    public class ShopManager : MonoBehaviour
    {
        [FormerlySerializedAs("itemPool"), SerializeField] private SerializedDictionary<RelicScriptableObjectBase, int> _itemPool = new();
        private readonly List<RelicScriptableObjectBase> _shopItemPool = new();

        public SerializedDictionary<DiceBase, int> dicePool = new();
        private readonly List<DiceBase> _shopDicePool = new();

        [SerializeField] private int _proposedDiceCount = 3;
        [SerializeField] private int _proposedRelicCount = 1;
        
        [SerializeField] private GameManager _gameManager;

        private int _shopIndex = -1;
        private int _invIndex = -1;
    
        private inventoryBehaviour _inv;

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
            for (int i = 0; i < _proposedDiceCount; i++)
            { 
                int randomItem = Random.Range(0, randItemPool.Count);
                RelicScriptableObjectBase randItem = randItemPool[randomItem];
                _itemPool[randItem] -= 1;
                _shopItemPool.Add(randItem);
            }
        
            List<DiceBase> rantDicePool = new();
            _shopDicePool.Clear();
            foreach ((DiceBase item, int quantity) in dicePool)
            {
                for (int i = 0; i < quantity; i++)
                {
                    rantDicePool.Add(item);
                }
            }
            rantDicePool = rantDicePool.OrderBy(x => Random.value).ToList();
            for (int i = 0; i < _proposedRelicCount; i++)
            {
                int randomItem = Random.Range(0, rantDicePool.Count);
                DiceBase randItem = rantDicePool[randomItem];
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

            foreach (DiceBase diceBase in _shopDicePool)
            {
                dicePool[diceBase] += 1;
            }
        }

        public void TakeDice(int shopIndex, int invIndex)
        {
            (_shopDicePool[shopIndex], _inv.DiceInventory[invIndex]) = (_inv.DiceInventory[invIndex], _shopDicePool[shopIndex]);
        
        }

        public void TakeItem(int shopIndex, int invIndex)
        {
            (_shopItemPool[shopIndex], _inv.ObjectsInventory[invIndex]) = (_inv.ObjectsInventory[invIndex], _shopItemPool[shopIndex]);
        }

        public void Choose(int index, bool isShop)
        {
            if (isShop) _shopIndex = index;
            else _invIndex = index;

            if (_shopIndex!= -1 && _invIndex != -1)
            {
                // if (FindAnyObjectByType<GameManager>().state == State.ShopDice)
                // {
                //     TakeDice(_shopIndex, _invIndex);
                // }
                // else if (FindAnyObjectByType<GameManager>().state== State.ShopObject)
                // {
                //     TakeItem(_shopIndex, _invIndex);
                // }


                (_shopIndex, _invIndex) = (-1, -1);
            }
        }

    }
}
