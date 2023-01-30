using Restaurant.Repositories;
namespace Restaurant.Services
{
    public class ChecksService
    {
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        public ChecksService()
        {
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
        }

        public async void SaveChecks(int orderId)
        {
            if (orderId == 0)
            {
                return;
            }
            var order = _orderRepository.GetOrderById(orderId);
            var orderItems = _orderItemRepository.GetOrderItemByOrderId(orderId);
            var path = $"../../../Checks/{order.OrderId}.txt";
            var chek = $"OrderNr: {order.OrderId}\nOrder data: {order.OrderDate: yy/MM/dd H:mm}\n{order.TableNumber} table\n{order.CustomersNumber} customers\nMeals:\n";
            orderItems.ForEach(x => chek += $" {x.Name} {x.Price:C2}\n");
            chek += $"         Total : {order.TotalCost:C2}";
            await WriteTextToFile(path, chek);
        }

        public async Task WriteTextToFile(string path, string text)
        {
            await File.WriteAllTextAsync(path, text);
        }


    }
}
