namespace OrderService.BLL.Models
{
    public class OrdersGettingRequestModel 
    {
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
    }
}
