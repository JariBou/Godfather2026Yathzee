using UnityEngine;
using UnityEngine.Serialization;

namespace _project.Scripts
{
    public abstract class EffectBase : ScriptableObject
    {
        [FormerlySerializedAs("_priority"),SerializeField] protected EffectPriority priority;

        public EffectPriority Priority => priority;

        public abstract Awaitable ApplyEffect(GameState gameState);

        public virtual string GetEffectDisplayIndicator()
        {
            return "EFFECT";
        }
    }
}