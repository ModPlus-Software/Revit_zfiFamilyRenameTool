namespace zfiFamilyRenameTool.ViewModel
{
    using ModPlusAPI.Mvvm;

    public class LogMessage : VmBase
    {
        public LogMessage(string title, string message, bool isError = false)
        {
            Message = message;
            IsError = isError;
            Title = title;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public bool IsError { get; set; }
    }
}