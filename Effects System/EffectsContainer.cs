using System.Collections.Generic;
using UnityEngine;

namespace EffectsSystem
{
    [System.Serializable]
    public class EffectsContainer
    {
        [field: SerializeReference] public List<Effect> Effects { get; set; } = new();
    }
}
