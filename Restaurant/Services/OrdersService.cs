using Restaurant.Models;
using Restaurant.Enum;
using Restaurant.Repositories;
using Restaurant.Interfaces;

namespace Restaurant.Services
{
    public class OrdersService : IOrderService
    {
        private readonly UiService _ui;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        public OrdersService(UiService ui)
        {
            _ui = ui;
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
        }

        public Order GetOrderByTableName(int tableNumber, bool isPaid)
        {
            var orderList = _orderRepository.GetOrdersList().Where(x => x.IsPaid == isPaid);
            return orderList.First(x => x.TableNumber == tableNumber);
        }
        public void DeleteOrder(int tableNumber)
        {
            if (tableNumber == 0) return;
            var order = GetOrderByTableName(tableNumber, isPaid: false);
            _orderRepository.DeleteOrder(order);

        }
        public void StartOrder(int customersNumber, int tableNumber)
        {
            if (tableNumber == 0) return;
            var order = new Order()
            {
                TableNumber = tableNumber,
                CustomersNumber = customersNumber,
                OrderDate = DateTime.Now,
                OrderId = Guid.NewGuid(),
                IsPaid = false
            };
            _orderRepository.AddOrder(order);
        }

        public int PayOrder(int orderId)
        {
            if (orderId == 0)
            {
                return 0;
            }
            var order = _orderRepository.GetOrderById(orderId);
            return CloseOrder(order.TableNumber);

        }

        private int CloseOrder(int tableNumber)
        {

            var orderList = new List<Order>(_orderRepository.GetOrdersList().Where(x => x.IsPaid == false));
            var order = orderList.First(x => x.TableNumber == tableNumber);
            _ui.ShowPayMenu(order);
            var email = _ui.AskEmail();
            if (email != "")
            {
                order.CustomerEmail = email;
                order.SendEmail = true;
            }
            order.IsPaid = true;
            order.PaidDate = DateTime.Now;
            _orderRepository.UpdateOrder(order);
            return order.TableNumber;
        }

        public int FindNotPaidOrder()
        {
            var orderList = new List<Order>(_orderRepository.GetOrdersList().Where(x => x.IsPaid == false));
            if (orderList.Count == 0)
            {
                _ui.OutputMessage(ErrorMessage.Order);
                return 0;
            }
            _ui.ShowOrdersList(orderList);
            var tableNr = _ui.AskChoose(@"\n Enter your table choose(0 exit):");
            if (CheckIsOrderInList(tableNr, orderList))
            {
                return orderList.First(x => x.TableNumber == tableNr).Id;
            }
            else
            {
                return 0;
            }
        }
        public bool CheckIsOrderInList(int tableNumber, List<Order> list)
        {
            var count = list.Count(x => x.TableNumber == tableNumber);
            return count != 0;
        }

        public void ShowAllOrder(int orderId)

        {
            if (orderId == 0)
            {
                return;
            }
            var order = _orderRepository.GetOrderById(orderId);
            var orderItems = _orderItemRepository.GetOrderItemByOrderId(orderId);
            _ui.ShowOrderInfo(order, orderItems);
        }

        public void CalculateTotal(int orderId)
        {
            if (orderId == 0)
            {
                return;
            }
            var order = _orderRepository.GetOrderById(orderId);
            var orderItems = _orderItemRepository.GetOrderItemByOrderId(orderId);

            foreach (var item in orderItems)
            {
                order.TotalCost += item.Price;
            }
            _orderRepository.UpdateOrder(order);
        }
    }
}
