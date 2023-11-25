﻿namespace OrderService.BLL.CustomExceptions
{
    public class CustomInvalidOperationException : Exception
    {
        public CustomInvalidOperationException(string? message) : base(message)
        {
        }
    }
}
