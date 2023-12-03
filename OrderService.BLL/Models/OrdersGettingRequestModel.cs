namespace OrderService.BLL.Models
{
    public class OrdersGettingRequestModel 
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? NumbersFilter { get; set; }
        public string? ItemNamesFilter { get; set; }
        public string? ItemUnitsFilter { get; set; }
        public string? ProviderNamesFilter { get; set; }
    }
}
