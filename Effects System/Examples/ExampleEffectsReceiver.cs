using System;

namespace EffectsSystem
{
    public sealed class ExampleEffectsReceiver: IEffectsReceiver
    {
        private TestMultiplierController _multiplierController;

        public ExampleEffectsReceiver(TestMultiplierController multiplierController)
        {
            _multiplierController = multiplierController;
        }

        public bool CanReceive(Effect effect)
        {
            if(effect is not ExampleEffect exampleEffect)
                return false;

            var multipliedValue = exampleEffect.Multiplier * _multiplierController.Multiplier;

            return multipliedValue > TestMultiplierController.MinValue
                && multipliedValue < TestMultiplierController.MaxValue;
        }

        public void Receive(Effect effect)
        {
            if(CanReceive(effect) == false)
                throw new InvalidOperationException();

            var exampleEffect = effect as ExampleEffect;

            _multiplierController.Multiplier *= exampleEffect.Multiplier;
        }
    }
}
