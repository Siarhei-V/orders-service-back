namespace OrderService.BLL.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string? LogEvent { get; set; }
        public string? Description { get; set; }
    }
}
