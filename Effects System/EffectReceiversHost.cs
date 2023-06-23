using System.Collections.Generic;
using System.Linq;

namespace EffectsSystem
{
    // Use it if you haven't access to certain Effector
    // and you wants to send effect to suitable one or many
    public class EffectReceiversHost
    {
        private List<IEffectsReceiver> _receivers = new();
        public void AddReceiver(IEffectsReceiver receiver) => _receivers.Add(receiver);

        public void RemoveReceiver(IEffectsReceiver receiver) => _receivers.Remove(receiver);

        public void SendEffectToSuitableReceiver(Effect effect)
        {
            var suitableReceiver = GetFirstSuitableReceiverFor(effect);

            if(suitableReceiver is not null)
                suitableReceiver.Receive(effect);
            else
                throw new UnreceivedEffectException(effect);
        }

        public void SendEffectToAllSuitableReceivers(Effect effect)
        {
            var suitableReceivers = GetAllSuitableReceiversFor(effect);

            if(suitableReceivers is null || suitableReceivers.Count() < 1)
                throw new UnreceivedEffectException(effect);

            foreach(var receiver in suitableReceivers)
                receiver.Receive(effect);
        }

        public IEffectsReceiver GetFirstSuitableReceiverFor(Effect effect)
            => _receivers.FirstOrDefault(container => container.CanReceive(effect));

        public IEnumerable<IEffectsReceiver> GetAllSuitableReceiversFor(Effect effect)
            => _receivers.Where(effector => effector.CanReceive(effect));
    }
}
