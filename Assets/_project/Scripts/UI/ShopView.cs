using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField]
        private IndexedButtonView[] _offers =
            Array.Empty<IndexedButtonView>();

        [SerializeField]
        private IndexedButtonView[] _inventorySlots =
            Array.Empty<IndexedButtonView>();

        [SerializeField] private Button _finishButton;

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
            if (this.gameObject.name =="DiceShopScreen")
                FindAnyObjectByType<GameManager>().ChangeStateToDiceShop();
            else
                FindAnyObjectByType<GameManager>().ChangeStateToRelicShop();
            FindAnyObjectByType<ShopManager>().RefreshShop();
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
    }
}