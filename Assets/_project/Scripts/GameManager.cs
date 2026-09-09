using _project.Scripts.ScriptableObjects;
using UnityEngine;

namespace _project.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private ScoreDataScriptableObject _scoreData;
        private GameState _gameState;
    }
}