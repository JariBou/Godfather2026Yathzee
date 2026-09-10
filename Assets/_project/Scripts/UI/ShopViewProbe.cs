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

        [ContextMenu("Tester les visuels")]
        private void TestVisuals()
        {
            if (!Application.isPlaying || _shop == null)
                return;

            _shop.SetOffer(0, null, Color.magenta);
            _shop.SetInventoryItem(0, null, Color.red, "1");
            _shop.SetInventoryItem(1, null, Color.green, "2");
            _shop.SetInventoryItem(2, null, Color.blue, "3");
        }

        [ContextMenu("Masquer la premiere offre et vider la premiere case")]
        private void TestClear()
        {
            if (!Application.isPlaying || _shop == null)
                return;

            _shop.ClearOffer(0);
            _shop.ClearInventoryItem(0);
        }
    }
}