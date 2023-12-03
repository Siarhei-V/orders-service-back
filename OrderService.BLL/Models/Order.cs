namespace OrderService.BLL.Models
{
    public class Order : BaseEntity
    {
        public string? Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }

        public IEnumerable<OrderItem>? OrderItems { get; set; }
    }
}
