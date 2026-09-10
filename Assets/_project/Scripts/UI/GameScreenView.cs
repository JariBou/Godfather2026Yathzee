using System;
using System.Globalization;
using TMPro;
using UnityEngine;
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

        [Header("Visuels des dés")]
        [SerializeField] private ItemVisualView[] _inventoryDice = new ItemVisualView[5];

        [SerializeField] private ItemVisualView _rollingDie;

        [SerializeField] private ItemVisualView[] _resultDice = new ItemVisualView[5];

        [Header("Visuels des passifs")]
        [SerializeField] private ItemVisualView[] _inventoryPassives = new ItemVisualView[3];

        public event Action RollRequested;

        private static readonly CultureInfo DisplayCulture = CultureInfo.GetCultureInfo("fr-FR");

        private void OnEnable()
        {
            if (_rollButton != null)
                _rollButton.onClick.AddListener(HandleRollClicked);
        }

        private void OnDisable()
        {
            if (_rollButton != null)
                _rollButton.onClick.RemoveListener(HandleRollClicked);
        }

        public void SetScore(double score, double quota)
        {
            string scoreLabel = score.ToString("N0", DisplayCulture);
            string quotaLabel = quota.ToString("N0", DisplayCulture);

            _scoreText.text = $"{scoreLabel} / <b><u>{quotaLabel}</u></b>";
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
    }
}