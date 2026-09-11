using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;

namespace _project.Scripts.UI
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField]
        private IndexedButtonView[] _offers = Array.Empty<IndexedButtonView>();

        [SerializeField]
        private IndexedButtonView[] _inventorySlots = Array.Empty<IndexedButtonView>();

        [SerializeField] private Button _finishButton;

        [Header("Visuels")]
        [SerializeField] private ItemVisualView[] _offerVisuals = Array.Empty<ItemVisualView>();

        [SerializeField] private ItemVisualView[] _inventoryVisuals = Array.Empty<ItemVisualView>();
        [Header("Infobulles")] [SerializeField] private ItemTooltipTrigger[] _offerTooltips = Array.Empty<ItemTooltipTrigger>();

        [SerializeField] private ItemTooltipTrigger[] _inventoryTooltips = Array.Empty<ItemTooltipTrigger>();

        public event Action<int> OfferClicked;
        public event Action<int> InventorySlotClicked;
        public event Action FinishRequested;
        public UnityEvent FinishRequestedUnity;

        private void OnEnable()
        {
            foreach (var offer in _offers)
            {
                if (offer != null)
                    offer.Clicked += HandleOfferClicked;
            }

            foreach (var slot in _inventorySlots)
            {
                if (slot != null)
                    slot.Clicked += HandleInventorySlotClicked;
            }

            if (_finishButton != null)
                _finishButton.onClick.AddListener(HandleFinishClicked);
            var gameManager = FindAnyObjectByType<GameManager>();
            var shopManager = FindAnyObjectByType<ShopManager>();

            if (gameManager != null && shopManager != null)
            {
                if (gameObject.name == "DiceShopScreen")
                    gameManager.ChangeStateToDiceShop();
                else
                    gameManager.ChangeStateToRelicShop();

                shopManager.RefreshShop();
            }
        }

        private void OnDisable()
        {
            foreach (var offer in _offers)
            {
                if (offer != null)
                    offer.Clicked -= HandleOfferClicked;
            }

            foreach (var slot in _inventorySlots)
            {
                if (slot != null)
                    slot.Clicked -= HandleInventorySlotClicked;
            }

            if (_finishButton != null)
                _finishButton.onClick.RemoveListener(HandleFinishClicked);
        }

        private void HandleOfferClicked(int index)
        {
            OfferClicked?.Invoke(index);
        }

        private void HandleInventorySlotClicked(int index)
        {
            InventorySlotClicked?.Invoke(index);
        }

        private void HandleFinishClicked()
        {
            FinishRequested?.Invoke();
            FinishRequestedUnity?.Invoke();
        }

        public void SetOffer(int index, Sprite sprite, Color tint, string value = "")
        {
            _offerVisuals[index].SetVisual(sprite, tint, value);
            _offers[index].SetInteractable(true);
        }

        public void ClearOffer(int index)
        {
            _offers[index].SetInteractable(false);
            _offerVisuals[index].Clear();
        }

        public void SetInventoryItem(int index, Sprite sprite, Color tint, string value = "")
        {
            _inventoryVisuals[index].SetVisual(sprite, tint, value);
        }

        public void ClearInventoryItem(int index)
        {
            _inventoryVisuals[index].Clear();
        }

        public void SetInventorySlotInteractable(int index, bool interactable)
        {
            _inventorySlots[index].SetInteractable(interactable);
        }

        public void SetFinishInteractable(bool interactable)
        {
            _finishButton.interactable = interactable;
        }

        public void SetOfferTooltip(int index, DiceDataScriptableObject dice)
        {
            if (index >= 0 && index < _offerTooltips.Length &&
                _offerTooltips[index] != null)
            {
                _offerTooltips[index].SetData(dice);
            }
        }

        public void SetOfferTooltip(int index, RelicScriptableObjectBase relic)
        {
            if (index >= 0 && index < _offerTooltips.Length &&
                _offerTooltips[index] != null)
            {
                _offerTooltips[index].SetData(relic);
            }
        }

        public void SetInventoryTooltip(int index, DiceDataScriptableObject dice)
        {
            if (index >= 0 && index < _inventoryTooltips.Length &&
                _inventoryTooltips[index] != null)
            {
                _inventoryTooltips[index].SetData(dice);
            }
        }

        public void SetInventoryTooltip(int index, RelicScriptableObjectBase relic)
        {
            if (index >= 0 && index < _inventoryTooltips.Length &&
                _inventoryTooltips[index] != null)
            {
                _inventoryTooltips[index].SetData(relic);
            }
        }
    }
}