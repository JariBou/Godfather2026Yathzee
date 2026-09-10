using UnityEngine;

namespace _project.Scripts.UI
{
    public class GameScreenViewProbe : MonoBehaviour
    {
        [SerializeField] private GameScreenView _view;

        [ContextMenu("Tester l'affichage")]
        private void TestDisplay()
        {
            if (!Application.isPlaying || _view == null)
                return;

            _view.SetScore(123456789, 150000000);
            _view.SetTurn(12);
            _view.SetRollInteractable(false);

            for (int i = 0; i < 5; i++)
            {
                string value = (i + 1).ToString();
                _view.SetInventoryDie(i, null, Color.cyan, value);
                _view.ShowResultDie(i, null, Color.yellow, value);
            }

            _view.ShowRollingDie(null, Color.magenta, "6");

            _view.SetInventoryPassive(0, null, Color.red);
            _view.SetInventoryPassive(1, null, Color.green);
            _view.SetInventoryPassive(2, null, Color.blue);
        }

        [ContextMenu("Tester le masquage")]
        private void TestClear()
        {
            if (!Application.isPlaying || _view == null)
                return;

            _view.HideRollingDie();
            _view.HideAllResults();

            for (int i = 0; i < 5; i++)
                _view.ClearInventoryDie(i);

            for (int i = 0; i < 3; i++)
                _view.ClearInventoryPassive(i);

            _view.SetRollInteractable(true);
        }
    }
}