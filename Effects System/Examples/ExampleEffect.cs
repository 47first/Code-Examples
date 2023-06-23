using UnityEngine;

namespace EffectsSystem
{
    [System.Serializable]
    public class ExampleEffect: Effect
    {
        [field: SerializeField] public float Multiplier { get; set; }
    }
}
