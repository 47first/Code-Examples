using System.Collections.Generic;

namespace GameTasks
{
    public abstract class GameTaskComposite: GameTaskComponent
    {
        protected abstract IEnumerable<GameTaskComponent> Components { get; }
    }
}
