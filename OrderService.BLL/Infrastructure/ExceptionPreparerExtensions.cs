namespace OrderService.BLL.Infrastructure
{
    public static class ExceptionPreparerExtensions
    {
        /// <summary>
        /// Возвращает подготовленную к записи в лог строку с описанием исключения
        /// </summary>
        public static string PrepareException(this Exception exception)
        {
            return $"Message: {exception.Message}{Environment.NewLine}Stack: {exception.StackTrace}";
        }
    }
}
