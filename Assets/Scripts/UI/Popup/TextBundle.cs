namespace Assets.Scripts.UI
{
    public struct TextBundle
    {
        public TextModel model;
        public string value;

        public TextBundle(TextModel model, string value)
        {
            this.model = model;
            this.value = value;
        }
    }

}
