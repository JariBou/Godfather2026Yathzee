using TMPro;
using UnityEngine;

namespace _project.Scripts.UI
{
    public class TooltipView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tooltipText;

        public void Show(string title, string description)
        {
            _tooltipText.text = $"<b>{title}</b>\n{description}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetPosition(Vector2 anchoredPosition)
        {
            ((RectTransform)transform).anchoredPosition = anchoredPosition;
        }
    }
}