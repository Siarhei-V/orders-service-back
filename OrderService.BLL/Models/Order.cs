namespace OrderService.BLL.Models
{
    public class Order : BaseEntity
    {
        public string? Number { get; set; }
        public DateOnly Date { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
    }
}
