using UnityEngine;
using System.Collections.Generic;

namespace GameTasks
{
    [System.Serializable]
    public class DefaultCompositeModel
    {
        [field: SerializeReference] public List<object> Children { get; private set; } = new();
    }
}
