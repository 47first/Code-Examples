namespace GameTasks
{
    public sealed class ComponentModelAttribute: System.Attribute
    {
        public string ButtonTitle { get; private set; }

        public ComponentModelAttribute(string buttonTitle)
        {
            ButtonTitle = buttonTitle;
        }
    }
}
