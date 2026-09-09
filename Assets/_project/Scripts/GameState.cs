using _project.Scripts.DieLaunching;
using _project.Scripts.ScriptableObjects;

namespace _project.Scripts
{
    public class GameState
    {
        public DiceLauncher Launcher { get; private set; }

        public ScoreDataScriptableObject ScoreData { get; private set; }


        public GameState(DiceLauncher diceLauncher, ScoreDataScriptableObject scoreData)
        {
            ScoreData = scoreData;
            Launcher = diceLauncher;
        }
    }
}