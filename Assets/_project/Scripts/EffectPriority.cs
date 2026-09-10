using System;
using UnityEngine;

namespace _project.Scripts
{
    public enum EffectPriority
    {
        High,
        Medium,
        Low,
        [InspectorName("Lingering (After Score Calc)")]Lingering,
        // ReSharper disable once InconsistentNaming
        [Obsolete]COUNT, // Don't show in inspector
    }
}