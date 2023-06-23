using System;

namespace GameTasks
{
    public enum GameTaskComponentState
    {
        Inactive,
        Active,
        Completed,
        Failed
    }

    public abstract class GameTaskComponent
    {
        public event Action<GameTaskComponent> OnInactivate;
        public event Action<GameTaskComponent> OnActivate;
        public event Action<GameTaskComponent> OnComplete;
        public event Action<GameTaskComponent> OnFailure;

        private GameTaskComponentState _state = GameTaskComponentState.Inactive;

        public GameTaskComponentState State
        {
            get => _state;
            private set
            {
                if(_state == value)
                    return;

                _state = value;

                (Action<GameTaskComponent> onStateActing, Action onStateActed) stateActions = _state switch
                {
                    GameTaskComponentState.Inactive => (OnInactivate, OnInactivated),
                    GameTaskComponentState.Active => (OnActivate, OnActivated),
                    GameTaskComponentState.Completed => (OnComplete, OnCompleted),
                    GameTaskComponentState.Failed => (OnFailure, OnFail),
                    _ => throw new InvalidOperationException("Undefined component state")
                };

                stateActions.onStateActing?.Invoke(this);

                stateActions.onStateActed?.Invoke();
            }
        }

        public void Reset()
        {
            OnReset();

            Deactivate();
        }

        public void Activate() => State = GameTaskComponentState.Active;

        public void Deactivate() => State = GameTaskComponentState.Inactive;

        public void SetFailureState() => State = GameTaskComponentState.Failed;

        protected void Complete() => State = GameTaskComponentState.Completed;

        protected virtual void OnInactivated() { }

        protected virtual void OnActivated() { }

        protected virtual void OnCompleted() { }

        protected virtual void OnFail() { }

        protected virtual void OnReset() { }
    }
}
