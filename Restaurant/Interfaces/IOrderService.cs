using Restaurant.Models;

namespace Restaurant.Interfaces
{
    public interface IOrderService
    {
        public Order GetOrderByTableName(int tableNumber, bool isPaid);
        public void DeleteOrder(int tableNumber);
        public void StartOrder(int customersNumber, int tableNumber);
        public int PayOrder(int orderId);
        public int FindNotPaidOrder();
        public bool CheckIsOrderInList(int tableNumber, List<Order> list);

        public void ShowAllOrder(int orderId);
        public void CalculateTotal(int orderId);

    }
}
