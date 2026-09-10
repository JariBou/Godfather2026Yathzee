using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class ItemVisualView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        [Tooltip("Facultatif : laisser vide pour un objet sans valeur.")]
        [SerializeField] private TMP_Text _valueText;

        public void SetVisual(
            Sprite sprite,
            Color tint,
            string value = "")
        {
            _icon.sprite = sprite;
            _icon.color = tint;
            _icon.enabled = true;
            _icon.preserveAspect = sprite != null;

            if (_valueText != null)
            {
                _valueText.text = value;
                _valueText.enabled = !string.IsNullOrEmpty(value);
            }
        }

        public void Clear()
        {
            _icon.enabled = false;

            if (_valueText != null)
            {
                _valueText.text = string.Empty;
                _valueText.enabled = false;
            }
        }
    }
}