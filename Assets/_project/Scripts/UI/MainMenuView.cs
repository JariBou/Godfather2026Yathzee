using System;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;
using UnityEngine.Events;

namespace _project.Scripts.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        [Header("Record")]
        [SerializeField] private TMP_Text _highScoreText;

        public event Action PlayRequested;
        public UnityEvent PlayRequestedUnity;
        public event Action QuitRequested;
        public UnityEvent QuitRequestedUnity;

        private void OnEnable()
        {
            if (_playButton != null)
                _playButton.onClick.AddListener(HandlePlayClicked);

            if (_quitButton != null)
                _quitButton.onClick.AddListener(HandleQuitClicked);

            int prevScore = PlayerPrefs.GetInt("max_score", -1);
            int maxStage = PlayerPrefs.GetInt("max_stage", -1);
            if (maxStage > -1)
            {
                SetHighScore(prevScore, maxStage);
            }
            else
            {
                ShowNoHighScore();
            }
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
            PlayRequestedUnity?.Invoke();
        }

        private void HandleQuitClicked()
        {
            QuitRequested?.Invoke();
            QuitRequestedUnity?.Invoke();
            Application.Quit();
        }

        public void SetHighScore(double score, int turnReached)
        {
            string scoreLabel = score.ToString(
                "N0", CultureInfo.GetCultureInfo("fr-FR"));

            _highScoreText.text =
                $"<b>Stage Atteint<b>: {turnReached}\n" +
                $"<b>Meilleur Score<b>: {scoreLabel}";
            
            // _highScoreText.text =
                // $"<b>Meilleur score</b>\n" +
                // $"{scoreLabel}\n" +
                // $"Tour atteint : {turnReached}";
        }

        public void ShowNoHighScore()
        {
            SetHighScore(0, 0);
            // _highScoreText.text =
            //     "<b>Meilleur score</b>\nAucune partie termin�e";
        }
    }
}