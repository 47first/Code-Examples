using System;

namespace EffectsSystem
{
    public class EffectAttribute: Attribute
    {
        public string Title { get; private set; }
        public EffectAttribute(string title) => Title = title;
    }
}
