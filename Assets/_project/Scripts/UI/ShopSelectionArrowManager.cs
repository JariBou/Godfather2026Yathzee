using System;
using System.Collections.Generic;
using _project.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts.UI
{
    public class ShopSelectionArrowManager : MonoBehaviour
    {
        [SerializeField] private ShopView _diceShopView;
        [SerializeField] private ShopView _relicShopView;
        [SerializeField] private ShopManager _shopManager;

        [FormerlySerializedAs("_inventoryArrows"),SerializeField] private List<SelectionArrowScript> _diceInventoryArrows;
        [SerializeField] private List<SelectionArrowScript> _diceShopArrows;
        
        [SerializeField] private List<SelectionArrowScript> _relicInventoryArrows;
        [SerializeField] private List<SelectionArrowScript> _relicShopArrows;


        public void ResetAllDisplays()
        {
            foreach (SelectionArrowScript arrow in _diceInventoryArrows)
            {
                arrow.Deselect();
            }
            foreach (SelectionArrowScript arrow in _diceShopArrows)
            {
                arrow.Deselect();
            }
            foreach (SelectionArrowScript arrow in _relicShopArrows)
            {
                arrow.Deselect();
            }
            foreach (SelectionArrowScript arrow in _relicInventoryArrows)
            {
                arrow.Deselect();
            }
        }

        private void Awake()
        {
            _shopManager.Swapped += ResetAllDisplays;
            _shopManager.InventoryClicked += ShopManagerOnInventoryClicked;
            _shopManager.OfferClicked += ShopManagerOnOfferClicked;
            
            _diceShopView.Refreshed += ResetAllDisplays;
            _relicShopView.Refreshed += ResetAllDisplays;
            
            // _diceShopView.OfferClicked += DiceShopViewOnOfferClicked;
            // _relicShopView.OfferClicked += RelicShopViewOnOfferClicked;
            //
            // _diceShopView.InventorySlotClicked += DiceInventorySlotClicked;
            // _relicShopView.InventorySlotClicked += RelicInventorySlotClicked;
        }

        private void ShopManagerOnOfferClicked(int index, State state)
        {
            switch (state)
            {
                case State.ShopDice:
                    DiceShopViewOnOfferClicked(index);
                    break;
                case State.ShopObject:
                    RelicShopViewOnOfferClicked(index);
                    break;
            }
        }

        private void ShopManagerOnInventoryClicked(int index, State state)
        {
            switch (state)
            {
                case State.ShopDice:
                    DiceInventorySlotClicked(index);
                    break;
                case State.ShopObject:
                    RelicInventorySlotClicked(index);
                    break;
            }
        }

        private void RelicInventorySlotClicked(int obj)
        {
            for (int i = 0; i < _relicInventoryArrows.Count; i++)
            {
                SelectionArrowScript arrow = _relicInventoryArrows[i];
                if (i == obj) arrow.Select();
                else arrow.Deselect();
            }
        }

        private void DiceInventorySlotClicked(int obj)
        {
            for (int i = 0; i < _diceInventoryArrows.Count; i++)
            {
                SelectionArrowScript arrow = _diceInventoryArrows[i];
                if (i == obj) arrow.Select();
                else arrow.Deselect();
            }
        }

        private void RelicShopViewOnOfferClicked(int obj)
        {
            for (int i = 0; i < _relicShopArrows.Count; i++)
            {
                SelectionArrowScript arrow = _relicShopArrows[i];
                if (i == obj) arrow.Select();
                else arrow.Deselect();
            }
        }

        private void DiceShopViewOnOfferClicked(int obj)
        {
            for (int i = 0; i < _diceShopArrows.Count; i++)
            {
                SelectionArrowScript arrow = _diceShopArrows[i];
                if (i == obj) arrow.Select();
                else arrow.Deselect();
            }
        }
    }
}