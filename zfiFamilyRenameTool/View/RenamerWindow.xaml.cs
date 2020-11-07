namespace zfiFamilyRenameTool.View
{
    using ModPlusAPI.Abstractions;

    public partial class RenamerWindow : IClosable
    {
        public RenamerWindow()
        {
            InitializeComponent();

            Title = ModPlusAPI.Language.GetFunctionLocalName(ModPlusConnector.Instance);
        }
    }
}