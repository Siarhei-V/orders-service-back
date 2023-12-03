using OrderService.BLL.Models;

namespace OrderService.BLL.Infrastructure
{
    public interface IBackgroundDataHandler
    {
        /// <summary>
        /// Fire and forget метод записи исключения в лог
        /// </summary>
        /// <param name="exception">Сохраняемое исключение</param>
        /// <param name="attemptsNumber">Количество попыток записи исключения в лог</param>
        void HandleLog(Exception exception, int attemptsNumber = 9);
    }
}