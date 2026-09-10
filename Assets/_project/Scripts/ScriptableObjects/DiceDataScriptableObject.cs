using _project.Scripts.Die;
using UnityEngine;

namespace _project.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DiceDataScriptableObject", menuName = "Chaos Yahtzee/Dice Data")]
    public class DiceDataScriptableObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private DiceBase _prefab;

        public string Name => _name;
        public Sprite Icon => _icon;
        public DiceBase Prefab => _prefab;
        public string Description => _description;
    }
}