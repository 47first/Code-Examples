using System;
using System.Collections.Generic;

namespace GameTasks
{
    public interface IComponentCreator
    {
        public void AddComponentBuilder(IComponentBuilder gameTaskStepBuilder);
        public GameTaskComponent CreateComponent(object model);
    }

    public sealed class ComponentsCreator: IComponentCreator
    {
        private readonly Dictionary<Type, IComponentBuilder> _componentBuilders = new();

        public void AddComponentBuilder(IComponentBuilder componentBuilder)
        {
            var componentType = componentBuilder.GetType().BaseType.GetGenericArguments()[0];

            bool hasComponentAdded = _componentBuilders.TryAdd(componentType, componentBuilder);

            if(hasComponentAdded == false)
                throw new InvalidOperationException("There is GameTaskComponentBuilder with this component type already!");
        }

        public GameTaskComponent CreateComponent(object model)
        {
            if(_componentBuilders.TryGetValue(model.GetType(), out IComponentBuilder builder))
                return builder.CreateGameTaskComponent(model);

            throw new InvalidOperationException("There is no suit GameTaskComponentBuider! Add builder to GameTaskComponentBuildersController");
        }
    }
}
