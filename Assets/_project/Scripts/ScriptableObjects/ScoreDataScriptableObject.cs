using NaughtyAttributes;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Chaos Yahtzee/Score Data", fileName = "ScoreDataScriptableObject")]
    public class ScoreDataScriptableObject : ScriptableObject
    {
        [SerializeField, InfoBox("initial_target_score * stage ^ multiplier \n(Minimum of 1)")] private int _initialTargetScore = 1;
        [SerializeField, Range(0.7f, 2.5f)] private float _perStageMultiplier = 1.2f;
        
        public int InitialTargetScore => _initialTargetScore;
        public float PerStageMultiplier => _perStageMultiplier;


        public float GetTargetScoreForStage(int stage)
        {
            // return _initialTargetScore * Mathf.Pow(_perStageMultiplier, stage);
            return Mathf.Max(_initialTargetScore * Mathf.Pow(stage, _perStageMultiplier), 1);
        }
        
        public int GetTargetScoreForStageInt(int stage)
        {
            // return (int)(_initialTargetScore * Mathf.Pow(_perStageMultiplier, stage));
            return (int)(GetTargetScoreForStage(stage));
        }
    }
}