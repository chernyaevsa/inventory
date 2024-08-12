namespace Service.Views.BaseViews 
{
    public class BaseResponseView
    {
        public string Message { get; set; } = "";
        public int StatusCode { get; set; } = 200;
        public object? Content { get; set; } = null;

        public BaseResponseView(string message, int statusCode, object? content)
        {
            Message = message;
            StatusCode = statusCode;
            Content = content;
        }
    }
}