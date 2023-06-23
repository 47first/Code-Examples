using System;

namespace GameTasks
{
    public abstract class ComponentBuilder<TModel>: IComponentBuilder
    {
        public abstract GameTaskComponent CreateGameTaskComponent(TModel model);
        GameTaskComponent IComponentBuilder.CreateGameTaskComponent(object model)
        {
            if(model is TModel tmodel)
                return CreateGameTaskComponent(tmodel);

            else
                throw new ArgumentException("Invalid model type");
        }
    }

    public interface IComponentBuilder
    {
        internal GameTaskComponent CreateGameTaskComponent(object model);
    }
}
