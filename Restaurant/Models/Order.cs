namespace Restaurant.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TableNumber { get; set; }
        public int? CustomersNumber { get; set; }
        public double TotalCost { get; set; }
        public bool SendEmail { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }

    }
}
