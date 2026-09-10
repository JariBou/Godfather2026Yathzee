using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _runSummary;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;

        public event Action RetryRequested;
        public event Action MainMenuRequested;

        private static readonly CultureInfo DisplayCulture =
            CultureInfo.GetCultureInfo("fr-FR");

        private void OnEnable()
        {
            if (_retryButton != null)
                _retryButton.onClick.AddListener(HandleRetryClicked);

            if (_mainMenuButton != null)
                _mainMenuButton.onClick.AddListener(HandleMainMenuClicked);
        }

        private void OnDisable()
        {
            if (_retryButton != null)
                _retryButton.onClick.RemoveListener(HandleRetryClicked);

            if (_mainMenuButton != null)
                _mainMenuButton.onClick.RemoveListener(HandleMainMenuClicked);
        }

        public void SetSummary(int turn, double score, double quota)
        {
            string scoreLabel = score.ToString("N0", DisplayCulture);
            string quotaLabel = quota.ToString("N0", DisplayCulture);

            _runSummary.text =
                $"Quota non atteint\n" +
                $"Tour atteint : {turn}\n" +
                $"Score du tour : {scoreLabel} / {quotaLabel}";
        }

        private void HandleRetryClicked()
        {
            RetryRequested?.Invoke();
        }

        private void HandleMainMenuClicked()
        {
            MainMenuRequested?.Invoke();
        }
    }
}