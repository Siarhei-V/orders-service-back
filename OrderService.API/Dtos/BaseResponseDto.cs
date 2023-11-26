namespace OrderService.API.Dtos
{
    public class BaseResponseDto
    {
        public int Status { get; set; } = StatusCodes.Status200OK;
        public string? Message { get; set; } = "Ok";
    }
}
