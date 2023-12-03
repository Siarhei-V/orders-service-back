namespace OrderService.BLL.Models
{
    public class Filter
    {
        public IEnumerable<string?>? Numbers { get; set; }
        public IEnumerable<string?>? ItemNames { get; set; }
        public IEnumerable<string?>? ItemUnits { get; set; }
        public IEnumerable<string?>? ProviderNames { get; set; }
    }
}
