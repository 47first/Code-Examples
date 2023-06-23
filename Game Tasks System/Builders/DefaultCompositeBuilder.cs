namespace GameTasks
{
    public sealed class DefaultCompositeBuilder: ComponentBuilder<DefaultCompositeModel>
    {
        private IComponentCreator _componentCreator;
        public DefaultCompositeBuilder(IComponentCreator componentCreator)
        {
            _componentCreator = componentCreator;
        }

        public override GameTaskComponent CreateGameTaskComponent(DefaultCompositeModel model)
        {
            var composite = new DefaultComposite();

            foreach(var childModel in model.Children)
            {
                var childComponent = _componentCreator.CreateComponent(childModel);

                composite.AddComponent(childComponent);
            }

            return composite;
        }
    }
}
