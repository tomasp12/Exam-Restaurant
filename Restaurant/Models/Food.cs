using Restaurant.Interfaces;

namespace Restaurant.Models
{
    public class Food : IMenuItems
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

    }
}
