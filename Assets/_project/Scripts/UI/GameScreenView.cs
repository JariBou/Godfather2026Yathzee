using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _project.Scripts.UI
{
    public class GameScreenView : MonoBehaviour
    {
        [Header("Textes")]
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _turnText;

        [Header("Actions")]
        [SerializeField] private Button _rollButton;

        [Header("Visuels des d�s")]
        [SerializeField] private ItemVisualView[] _inventoryDice = new ItemVisualView[5];

        [SerializeField] private ItemVisualView _rollingDie;

        [SerializeField] private ItemVisualView[] _resultDice = new ItemVisualView[5];

        [Header("Visuels des passifs")]
        [SerializeField] private ItemVisualView[] _inventoryPassives = new ItemVisualView[3];

        [SerializeField] private GameObject _background;
        [SerializeField] private GameManager _gameManager;
        
        public UnityEvent gameReset;
        public UnityEvent rollRequestedUnity;
        
        public event Action RollRequested;

        private static readonly CultureInfo DisplayCulture = CultureInfo.GetCultureInfo("fr-FR");

        private void OnEnable()
        {
            if (_rollButton != null)
                _rollButton.onClick.AddListener(HandleRollClicked);

            _gameManager.NextTurn();
            SetTurn(_gameManager.CurrentStage+1);
            SetScore(0, _gameManager.ScoreData.GetTargetScoreForStageInt(_gameManager.CurrentStage));
            _background.SetActive(false);
            gameReset?.Invoke();
            RefreshInventoryVisuals();
        }

        private void OnDisable()
        {
            if (_rollButton != null)
                _rollButton.onClick.RemoveListener(HandleRollClicked);
            
            _background.SetActive(true);
        }

        public void SetScore(double score, double quota)
        {
            string scoreLabel = score.ToString("N0", DisplayCulture);
            string quotaLabel = quota.ToString("N0", DisplayCulture);

            _scoreText.text = $"{scoreLabel} / <b><u>{quotaLabel}</u></b>";
        }

        public void SetScore(int score, int target)
        {
            _scoreText.text = $"{score} / <b><u>{target}</u></b>";
        }

        public void SetTurn(int turn)
        {
            _turnText.text = $"Tour {turn}";
        }

        public void SetRollInteractable(bool interactable)
        {
            _rollButton.interactable = interactable;
        }

        private void HandleRollClicked()
        {
            RollRequested?.Invoke();
            rollRequestedUnity?.Invoke();
        }

        public void SetInventoryDie(
    int index, Sprite sprite, Color tint, string value = "")
        {
            _inventoryDice[index].SetVisual(sprite, tint, value);
        }

        public void ClearInventoryDie(int index)
        {
            _inventoryDice[index].Clear();
        }

        public void ShowRollingDie(
            Sprite sprite, Color tint, string value = "")
        {
            _rollingDie.SetVisual(sprite, tint, value);
            _rollingDie.gameObject.SetActive(true);
        }

        public void HideRollingDie()
        {
            _rollingDie.gameObject.SetActive(false);
        }

        public void ShowResultDie(
            int index, Sprite sprite, Color tint, string value)
        {
            _resultDice[index].SetVisual(sprite, tint, value);
            _resultDice[index].gameObject.SetActive(true);
        }

        public void HideAllResults()
        {
            foreach (var result in _resultDice)
            {
                result.gameObject.SetActive(false);
            }
        }

        public void SetInventoryPassive(int index, Sprite sprite, Color tint)
        {
            _inventoryPassives[index].SetVisual(sprite, tint);
        }

        public void ClearInventoryPassive(int index)
        {
            _inventoryPassives[index].Clear();
        }

        public void RefreshInventoryVisuals()
        {
            if (_gameManager == null)
                return;

            for (int i = 0; i < _inventoryDice.Length; i++)
            {
                var dice = i < _gameManager.Inventory.Count
                    ? _gameManager.Inventory[i]
                    : null;

                if (dice != null)
                    SetInventoryDie(i, dice.Icon, Color.white, "");
                else
                    ClearInventoryDie(i);
            }

            for (int i = 0; i < _inventoryPassives.Length; i++)
            {
                var relic = i < _gameManager.Relics.Count
                    ? _gameManager.Relics[i]
                    : null;

                if (relic != null)
                    SetInventoryPassive(i, relic.Icon, Color.white);
                else
                    ClearInventoryPassive(i);
            }
        }
    }
}