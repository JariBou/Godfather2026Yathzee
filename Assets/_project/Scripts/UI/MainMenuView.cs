using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        public event Action PlayRequested;
        public event Action QuitRequested;

        private void OnEnable()
        {
            if (_playButton != null)
                _playButton.onClick.AddListener(HandlePlayClicked);

            if (_quitButton != null)
                _quitButton.onClick.AddListener(HandleQuitClicked);
        }

        private void OnDisable()
        {
            if (_playButton != null)
                _playButton.onClick.RemoveListener(HandlePlayClicked);

            if (_quitButton != null)
                _quitButton.onClick.RemoveListener(HandleQuitClicked);
        }

        private void HandlePlayClicked()
        {
            PlayRequested?.Invoke();
        }

        private void HandleQuitClicked()
        {
            QuitRequested?.Invoke();
        }
    }
}