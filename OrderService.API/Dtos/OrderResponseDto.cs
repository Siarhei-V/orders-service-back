using OrderService.BLL.Models;

namespace OrderService.API.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
