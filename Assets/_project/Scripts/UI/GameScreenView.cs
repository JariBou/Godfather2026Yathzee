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

        [SerializeField] private GameObject _background;

        [FormerlySerializedAs("GameReset")] public UnityEvent gameReset;
        [FormerlySerializedAs("RollRequestedUnity")] public UnityEvent rollRequestedUnity;

        public event Action RollRequested;

        public GameManager gm;


        public GameObject[] diceInvGame;
        public GameObject[] relicInvGame;


        private static readonly CultureInfo DisplayCulture =
            CultureInfo.GetCultureInfo("fr-FR");

        private void OnEnable()
        {
            if (_rollButton != null)
                _rollButton.onClick.AddListener(HandleRollClicked);
            
            _background.SetActive(false);
            gameReset?.Invoke();

            for (int i = 0; i < 5; i++)
            {
                refreshDiceVisual(i);
            }
        }

        private void OnDisable()
        {
            if (_rollButton != null)
                _rollButton.onClick.RemoveListener(HandleRollClicked);
            
            _background.SetActive(true);

        }

        public void SetScore(float score, float quota)
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
            rollRequestedUnity?.Invoke();
        }



        public void refreshDiceVisual(int index)
        {
            diceInvGame[index].GetComponent<Image>().sprite = gm.Inventory[index].Icon;
        }
        public void refreshRelicVisual(int index)
        {
            relicInvGame[index].GetComponent<Image>().sprite = gm.Relics[index].Icon;
        }
    }
}