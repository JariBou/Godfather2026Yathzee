using TMPro;
using UnityEngine;

namespace _project.Scripts.Effects.UIScore
{
    public class ScoreEffectDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void Setup(string display)
        {
            _scoreText.text = display;
        }

        public void DestroyFx() => Destroy(gameObject);
    }
}