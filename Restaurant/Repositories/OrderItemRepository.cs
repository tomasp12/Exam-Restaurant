using Restaurant.Models;
namespace Restaurant.Repositories
{
    public class OrderItemRepository
    {

        public List<OrderItem> GetOrdersItemsList()
        {
            DbRepository<OrderItem> data = new();
            return data.GetAll();
        }

        public void AddOrderItem(OrderItem order)
        {
            DbRepository<OrderItem> data = new();
            data.Insert(order);
        }

        public void UpdateOrderItem(OrderItem order)
        {
            DbRepository<OrderItem> data = new();
            data.Update(order);
        }

        public List<OrderItem> GetOrderItemByOrderId(int orderId)
        {
            DbRepository<OrderItem> data = new();
            return new List<OrderItem>(data.GetAll().Where(x => x.OrderId == orderId));
        }
    }
}
