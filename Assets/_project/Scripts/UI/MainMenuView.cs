using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

namespace _project.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        [Header("Record")]
        [SerializeField] private TMP_Text _highScoreText;

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

        public void SetHighScore(double score, int turnReached)
        {
            string scoreLabel = score.ToString(
                "N0", CultureInfo.GetCultureInfo("fr-FR"));

            _highScoreText.text =
                $"<b>Meilleur score</b>\n" +
                $"{scoreLabel}\n" +
                $"Tour atteint : {turnReached}";
        }

        public void ShowNoHighScore()
        {
            _highScoreText.text =
                "<b>Meilleur score</b>\nAucune partie terminée";
        }
    }
}