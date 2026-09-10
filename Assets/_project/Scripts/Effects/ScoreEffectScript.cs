using TMPro;
using UnityEngine;

namespace _project.Scripts.Effects
{
    public class ScoreEffectScript : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;

        public void Setup(int score)
        {
            _text.text = (score >= 0 ? "+ " : "- ") + score;
        }

        public void OnAnimFinished()
        {
            Destroy(gameObject);
        }
    }
}