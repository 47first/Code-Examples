namespace EffectsSystem
{
    public interface IEffectsReceiversHost
    {
        public void AddReceiver(IEffectsReceiver receiver);
        public void RemoveReceiver(IEffectsReceiver receiver);
        public void SendEffectToSuitableReceiver(Effect effect);
        public void SendEffectToAllSuitableReceivers(Effect effect);
    }
}
