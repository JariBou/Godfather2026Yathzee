using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _runSummary;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;

        public event Action RetryRequested;
        public UnityEvent RetryRequestedUnity;
        public event Action MainMenuRequested;
        public UnityEvent MenuRequestedUnity;

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

        public void OnGameLost(GameState state)
        {
            SetSummary(state.CurrentStage, state.CurrentRoundScore, state.TargetRoundScore);
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
            RetryRequestedUnity?.Invoke();
        }

        private void HandleMainMenuClicked()
        {
            MainMenuRequested?.Invoke();
            MenuRequestedUnity?.Invoke();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}