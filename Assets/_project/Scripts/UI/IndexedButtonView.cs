using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class IndexedButtonView : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _index;
        [SerializeField] private Button _button;

        public int Index => _index;
        public event Action<int> Clicked;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClicked);
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void HandleClicked()
        {
            Clicked?.Invoke(_index);
        }
    }
}