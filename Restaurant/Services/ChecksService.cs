using Restaurant.Repositories;
namespace Restaurant.Services
{
    public class ChecksService
    {
        private readonly UiService _ui;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        public ChecksService(UiService ui)
        {
            _ui = ui;
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
            var check = $"OrderNr: {order.OrderId}\nOrder data: {order.OrderDate: yy/MM/dd H:mm}\n{order.TableNumber} table\n{order.CustomersNumber} customers\nMeals:\n";
            orderItems.ForEach(x => check += $" {x.Name} {x.Price:C2}\n");
            check += $"       Total : {order.TotalCost:C2}";
            await WriteTextToFile(path, check);
        }
        public string LoadCheck(string orderId)
        {
            try
            {
                string filePath = $"../../../Checks/{orderId}.txt";
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                _ui.OutputText("Error reading file: " + ex.Message);
                _ui.PauseOutput();
            }
            return "";

        }
        public async Task WriteTextToFile(string path, string text)
        {
            await File.WriteAllTextAsync(path, text);
        }


    }
}
