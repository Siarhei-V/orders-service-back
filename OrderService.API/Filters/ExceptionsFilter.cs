using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderService.API.Dtos;
using OrderService.BLL.CustomExceptions;
using OrderService.BLL.Infrastructure;

namespace OrderService.API.Filters
{
    public class ExceptionsFilter : IActionFilter
    {
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public ExceptionsFilter(IBackgroundDataHandler backgroundDataHandler) => _backgroundDataHandler = backgroundDataHandler;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var ex = context.Exception;

            if (ex == null)
                return;

            if (ex is CustomInvalidOperationException)
            {
                var response = new ResponseDto { Status = StatusCodes.Status500InternalServerError, Message = ex.Message };
                context.Result = new ObjectResult(response) { StatusCode = response.Status };
            }
            else if (ex is CustomArgumentException)
            {
                var response = new ResponseDto { Status = StatusCodes.Status400BadRequest, Message = ex.Message };
                context.Result = new ObjectResult(response) { StatusCode = response.Status };
            }
            else
            {
                _backgroundDataHandler.HandleLog(ex);
                var response = new ResponseDto { Status = StatusCodes.Status500InternalServerError, Message = "Ошибка сервера" };
                context.Result = new ObjectResult(response) { StatusCode = response.Status };
            }

            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
