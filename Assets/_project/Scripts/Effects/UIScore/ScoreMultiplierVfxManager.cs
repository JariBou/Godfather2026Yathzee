using UnityEngine;

namespace _project.Scripts.Effects.UIScore
{
    public class ScoreMultiplierVfxManager : MonoBehaviour
    {
        [SerializeField] private ScoreEffectDisplay _effectPrefab;
        [SerializeField] private Transform _parent;

        public void ShowVfx(string display)
        {
            ScoreEffectDisplay scoreEffectScript = Instantiate(_effectPrefab, _parent);
            scoreEffectScript.Setup(display);
        }
    }
}