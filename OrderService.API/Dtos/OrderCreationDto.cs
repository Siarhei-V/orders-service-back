using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Dtos
{
    public class OrderCreationDto
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public int ProviderId { get; set; }
        [Required]
        public IEnumerable<OrderItemCreationModel> OrderItems { get; set; }
    }

    public class OrderItemCreationModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}
