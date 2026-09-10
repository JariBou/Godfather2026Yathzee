using UnityEngine;

namespace _project.Scripts.ScriptableObjects.Relics
{
    public abstract class RelicScriptableObjectBase : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField, TextArea] private string _description;

        public string Name => _name;
        public string Description => _description;

        public Sprite Icon => _icon;

        public abstract void ApplyEffect(GameState gamestate);
    }
}