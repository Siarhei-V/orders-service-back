using System.ComponentModel.DataAnnotations;

namespace OrderService.API.Dtos
{
    public class OrderUpdateDto : OrderCreationDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public IEnumerable<OrderItemUpdateModel> OrderItems { get; set; }

    }

    public class OrderItemUpdateModel : OrderItemCreationModel
    {
        [Required]
        public int Id { get; set; }
    }
}
