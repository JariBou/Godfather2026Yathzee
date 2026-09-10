using System;

namespace _project.Scripts
{
    public enum EffectPriority
    {
        High,
        Medium,
        Low,
        Lingering,
        // ReSharper disable once InconsistentNaming
        [Obsolete]COUNT, // Don't show in inspector
    }
}