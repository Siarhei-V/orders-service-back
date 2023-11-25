namespace OrderService.BLL.CustomExceptions
{
    public class CustomArgumentException : Exception
    {
        public CustomArgumentException(string? message) : base(message)
        {
        }
    }
}
