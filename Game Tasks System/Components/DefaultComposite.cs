using System;
using System.Collections.Generic;

namespace GameTasks
{
    public class DefaultComposite: GameTaskComposite
    {
        private readonly List<GameTaskComponent> _components = new();
        private int _activeComponentIndex = 0;

        protected override IEnumerable<GameTaskComponent> Components => _components;

        protected GameTaskComponent ActiveComponent => _components[_activeComponentIndex];

        public void AddComponent(GameTaskComponent component)
        {
            if(State != GameTaskComponentState.Inactive)
                throw new InvalidOperationException("Can't add task component, when composite is activated");

            _components.Add(component);
        }

        protected override void OnActivated() => ActivateComponent(ActiveComponent);

        private void ActivateComponent(GameTaskComponent component)
        {
            component.OnComplete += OnComponentComplete;
            component.Activate();
        }

        private void OnComponentComplete(GameTaskComponent sender)
        {
            UnsubscribeActiveComponent();

            if(++_activeComponentIndex >= _components.Count)
                Complete();

            else
                ActivateComponent(ActiveComponent);
        }

        protected void UnsubscribeActiveComponent() => ActiveComponent.OnComplete -= OnComponentComplete;

        protected override void OnReset()
        {
            UnsubscribeActiveComponent();

            _activeComponentIndex = 0;

            foreach(var component in _components)
                component.Reset();
        }

        protected override void OnFail()
        {
            UnsubscribeActiveComponent();

            foreach(var component in _components)
                component.SetFailureState();
        }
    }
}
