using _project.Scripts.ScriptableObjects.Dice;
using _project.Scripts.ScriptableObjects.Relics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _project.Scripts.UI
{
    public class ItemTooltipTrigger : MonoBehaviour,
        IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
    {
        [SerializeField] private TooltipView _tooltip;
        [SerializeField] private DiceDataScriptableObject _dice;
        [SerializeField] private RelicScriptableObjectBase _relic;

        private bool _hovered;
        private Vector2 _pointerPosition;

        public void SetData(DiceDataScriptableObject dice)
        {
            _dice = dice;
            _relic = null;
            RefreshTooltip();
        }

        public void SetData(RelicScriptableObjectBase relic)
        {
            _relic = relic;
            _dice = null;
            RefreshTooltip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovered = true;
            _pointerPosition = eventData.position;
            RefreshTooltip();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _pointerPosition = eventData.position;

            if (_hovered && _tooltip != null &&
                (_dice != null || _relic != null))
            {
                _tooltip.SetScreenPosition(_pointerPosition);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltip();
        }

        private void OnDisable()
        {
            HideTooltip();
        }

        private void RefreshTooltip()
        {
            if (!_hovered || _tooltip == null)
                return;

            if (_dice == null && _relic == null)
            {
                _tooltip.Hide();
                return;
            }

            string title = _dice != null ? _dice.Name : _relic.Name;
            string description = _dice != null
                ? _dice.Description
                : _relic.Description;

            if (string.IsNullOrWhiteSpace(title))
                title = _dice != null ? _dice.name : _relic.name;

            _tooltip.Show(title, description);
            _tooltip.SetScreenPosition(_pointerPosition);
        }

        private void HideTooltip()
        {
            if (_hovered && _tooltip != null)
                _tooltip.Hide();

            _hovered = false;
        }
    }
}