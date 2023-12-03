namespace OrderService.BLL.Models
{
    public class OrdersGettingRequestModel 
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public IEnumerable<string>? NumbersFilter { get; set; }
        public IEnumerable<string>? ItemNamesFilter { get; set; }
        public IEnumerable<string>? ItemUnitsFilter { get; set; }
        public IEnumerable<string>? ProviderNamesFilter { get; set; }
    }
}
