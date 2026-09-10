using UnityEngine;

namespace _project.Scripts.UI
{
    public class ShopViewProbe : MonoBehaviour
    {
        [SerializeField] private ShopView _shop;
        [SerializeField] private string _label = "Marché";

        private void OnEnable()
        {
            if (_shop == null)
                return;

            _shop.OfferClicked += HandleOffer;
            _shop.InventorySlotClicked += HandleSlot;
            _shop.FinishRequested += HandleFinish;
        }

        private void OnDisable()
        {
            if (_shop == null)
                return;

            _shop.OfferClicked -= HandleOffer;
            _shop.InventorySlotClicked -= HandleSlot;
            _shop.FinishRequested -= HandleFinish;
        }

        private void HandleOffer(int index)
        {
            Debug.Log($"[{_label}] Offre : {index}", this);
        }

        private void HandleSlot(int index)
        {
            Debug.Log($"[{_label}] Emplacement : {index}", this);
        }

        private void HandleFinish()
        {
            Debug.Log($"[{_label}] Terminer", this);
        }
    }
}