using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class TooltipView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tooltipText;
        [SerializeField] private Vector2 _offset = new(20f, 20f);
        [SerializeField] private float _screenMargin = 12f;

        private readonly Vector3[] _corners = new Vector3[4];

        public void Show(string title, string description)
        {
            _tooltipText.text = $"<b>{title}</b>\n{description}";
            gameObject.SetActive(true);

            transform.SetAsLastSibling();

            foreach (var graphic in GetComponentsInChildren<Graphic>(true))
                graphic.raycastTarget = false;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector2 anchoredPosition)
        {
            ((RectTransform)transform).anchoredPosition = anchoredPosition;
        }

        public void SetScreenPosition(Vector2 screenPosition)
        {
            var rect = (RectTransform)transform;
            var parent = rect.parent as RectTransform;
            var canvas = GetComponentInParent<Canvas>();

            if (parent == null || canvas == null)
                return;

            var camera = canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : canvas.worldCamera;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parent, screenPosition, camera, out var point))
                return;

            Canvas.ForceUpdateCanvases();

            rect.localPosition = new Vector3(
                point.x + _offset.x,
                point.y + _offset.y,
                rect.localPosition.z);

            rect.GetWorldCorners(_corners);

            Vector2 min = parent.InverseTransformPoint(_corners[0]);
            Vector2 max = min;

            for (int i = 1; i < _corners.Length; i++)
            {
                Vector2 corner =
                    parent.InverseTransformPoint(_corners[i]);

                min = Vector2.Min(min, corner);
                max = Vector2.Max(max, corner);
            }

            Rect bounds = parent.rect;
            Vector2 correction = Vector2.zero;

            if (max.x > bounds.xMax - _screenMargin)
                correction.x = bounds.xMax - _screenMargin - max.x;
            if (min.x + correction.x < bounds.xMin + _screenMargin)
                correction.x = bounds.xMin + _screenMargin - min.x;

            if (max.y > bounds.yMax - _screenMargin)
                correction.y = bounds.yMax - _screenMargin - max.y;
            if (min.y + correction.y < bounds.yMin + _screenMargin)
                correction.y = bounds.yMin + _screenMargin - min.y;

            rect.localPosition += (Vector3)correction;
        }
    }
}