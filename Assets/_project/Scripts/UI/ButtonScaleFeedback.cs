using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonScaleFeedback : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _visual;
        [SerializeField] private float _hoverScale = 1.1f;
        [SerializeField] private float _pressedScale = 0.95f;
        [SerializeField] private float _speed = 16f;

        private Button _button;
        private Vector3 _normalScale;
        private bool _hovered;
        private bool _pressed;

        private void Awake()
        {
            _button = GetComponent<Button>();

            if (_visual == null)
                _visual = (RectTransform)transform;

            _normalScale = _visual.localScale;
        }

        private void Update()
        {
            bool interactable = _button.isActiveAndEnabled &&
                                _button.IsInteractable();

            if (!interactable)
                _pressed = false;

            float factor = !interactable ? 1f
                : _pressed ? _pressedScale
                : _hovered ? _hoverScale
                : 1f;

            float blend = 1f - Mathf.Exp(-_speed * Time.unscaledDeltaTime);

            _visual.localScale = Vector3.Lerp(
                _visual.localScale,
                _normalScale * factor,
                blend);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovered = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left &&
                _button.IsInteractable())
            {
                _pressed = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                _pressed = false;
        }

        private void OnDisable()
        {
            _hovered = false;
            _pressed = false;

            if (_visual != null)
                _visual.localScale = _normalScale;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                _hovered = false;
                _pressed = false;
            }
        }
    }
}