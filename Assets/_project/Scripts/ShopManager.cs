using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using UnityEngine;
using UnityEngine.Serialization;
using _project.Scripts.UI;

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

        [SerializeField] private ShopView _diceShopView;
        [SerializeField] private ShopView _passiveShopView;

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
                RelicScriptableObjectBase randItem = randItemPool[i];
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
                DiceDataScriptableObject randItem = rantDicePool[i];
                dicePool[randItem] -= 1;
                _shopDicePool.Add(randItem);
            }

            RefreshDiceVisuals();
            RefreshPassiveVisuals();
        }

        private void RefreshDiceVisuals()
        {
            if (_diceShopView == null || _gameManager == null)
                return;

            for (int i = 0; i < _shopDicePool.Count; i++)
            {
                _diceShopView.SetOffer(
                    i, _shopDicePool[i].Icon, Color.white, "");
            }

            for (int i = 0; i < _gameManager.Inventory.Count; i++)
            {
                _diceShopView.SetInventoryItem(
                    i, _gameManager.Inventory[i].Icon, Color.white, "");
            }
        }

        private void RefreshPassiveVisuals()
        {
            if (_passiveShopView == null || _gameManager == null)
                return;

            for (int i = 0; i < _shopItemPool.Count; i++)
            {
                var relic = _shopItemPool[i];

                if (relic != null)
                    _passiveShopView.SetOffer(i, relic.Icon, Color.white, "");
                else
                    _passiveShopView.ClearOffer(i);
            }

            for (int i = 0; i < 3; i++)
            {
                var relic = i < _gameManager.Relics.Count
                    ? _gameManager.Relics[i]
                    : null;

                if (relic != null)
                    _passiveShopView.SetInventoryItem(
                        i, relic.Icon, Color.white, "");
                else
                    _passiveShopView.ClearInventoryItem(i);
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
            Debug.Log($"Taking dice from shop '{shopIndex}' to inventory '{invIndex}'");

            if (!(shopIndex >= 0 && shopIndex < _shopDicePool.Count))
            {
                Debug.LogError($"Shop Index was out of bounds: Expected [0, {_shopDicePool.Count - 1}] was '{shopIndex}'");
                return;
            }
            if (!(invIndex >= 0 && invIndex < _gameManager.Inventory.Count))
            {
                Debug.LogError($"Inventory Index was out of bounds: Expected [0, {_gameManager.Inventory.Count - 1}] was '{invIndex}'");
                return;
            }
            
            (_shopDicePool[shopIndex], _gameManager.Inventory[invIndex]) = (_gameManager.Inventory[invIndex], _shopDicePool[shopIndex]);

            RefreshDiceVisuals();
        }

        public void TakeItem(int shopIndex, int invIndex)
        {
            Debug.Log($"Taking relic from shop '{shopIndex}' to inventory '{invIndex}'");
            
            if (!(shopIndex >= 0 && shopIndex < _shopDicePool.Count))
            {
                Debug.LogError($"Shop Index was out of bounds: Expected [0, {_shopDicePool.Count - 1}] was '{shopIndex}'");
                return;
            }
            if (!(invIndex >= 0 && invIndex < _gameManager.Inventory.Count))
            {
                Debug.LogError($"Inventory Index was out of bounds: Expected [0, {_gameManager.Inventory.Count - 1}] was '{invIndex}'");
                return;
            }
            
            (_shopItemPool[shopIndex], _gameManager.Relics[invIndex]) = (_gameManager.Relics[invIndex], _shopItemPool[shopIndex]);

            RefreshPassiveVisuals();
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
