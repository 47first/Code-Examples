using System;

namespace EffectsSystem
{
    public class UnreceivedEffectException: Exception
    {
        public UnreceivedEffectException(Effect effect) : base($"No one {nameof(IEffectsReceiver)} in" +
            $" {nameof(EffectReceiversHost)} didn't receive {nameof(effect)}")
        { }
    }
}
