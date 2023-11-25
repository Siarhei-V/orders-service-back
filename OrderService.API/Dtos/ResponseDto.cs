namespace OrderService.API.Dtos
{
    internal class ResponseDto
    {
        public int Status { get; set; } = StatusCodes.Status200OK;
        public string? Message { get; set; } = "Ok";
        public object? Data { get; set; }
    }
}
