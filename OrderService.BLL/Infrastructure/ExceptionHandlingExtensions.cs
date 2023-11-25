using OrderService.BLL.CustomExceptions;

namespace OrderService.BLL.Infrastructure
{
    internal static class ExceptionHandlingExtensions
    {
        /// <summary>
        /// Исключения, которым доверяет, пробрасывает со сбросом трассировки стека. 
        /// Остальные исключения обрабатывает и генерирует исключение <see cref="CustomInvalidOperationException"/>
        /// </summary>
        /// <param name="exception">Обрабатываемое исключение</param>
        /// <param name="newExceptionMessage">Сообщение для вновь сгенерированного исключения</param>
        /// <param name="action">Делегат обработки исключения</param>
        /// <exception cref="CustomInvalidOperationException"></exception>
        public static void HandleException(this Exception exception, string newExceptionMessage, Action action)
        {
            if (exception is CustomInvalidOperationException)
                throw exception;

            action();
            throw new CustomInvalidOperationException(newExceptionMessage);
        }
    }
}
