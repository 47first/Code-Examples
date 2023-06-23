namespace EffectsSystem
{
    public interface IEffectsReceiver
    {
        public void Receive(Effect effect);
        public bool CanReceive(Effect effect);
    }
}
