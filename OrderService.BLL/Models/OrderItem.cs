using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.BLL.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string? Name { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
