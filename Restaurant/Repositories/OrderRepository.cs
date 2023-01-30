using Restaurant.Models;

namespace Restaurant.Repositories
{
    public class OrderRepository
    {
        public List<Order> GetOrdersList()
        {
            DbRepository<Order> data = new();
            return data.GetAll();
        }
        public void AddOrder(Order order)
        {
            DbRepository<Order> data = new();
            data.Insert(order);
        }

        public Order GetOrderById(int id)
        {
            DbRepository<Order> data = new();
            return data.GetById(id);
        }
        public void UpdateOrder(Order order)
        {
            DbRepository<Order> data = new();
            data.Update(order);
        }

        public void DeleteOrder(Order order)
        {
            DbRepository<Order> data = new();
            data.Delete(order);
        }
    }
}
