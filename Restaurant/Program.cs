using Restaurant.Enum;
using Restaurant.Models;
using Restaurant.Services;

namespace Restaurant
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] customers = { 5, 4, 2, 9, 4, 1, 3 };
            
            var userInterface = new UiService();
            var emailService = new EmailService(userInterface);
            var checksService = new ChecksService(userInterface);
            var tableService = new TablesService(userInterface);
            var foodMenu = new MenuService<Food>(userInterface);
            var drinkMenu = new MenuService<Drink>(userInterface);
            var orderService = new OrdersService(userInterface);
            var i = 0;
            while (true)
            {
                int tableNumber;
                int orderId;
                switch (userInterface.ShowMainMenu())
                {
                    case MenuChoose.ChooseTable:
                        tableNumber = tableService.ChooseFreeTable(customers[i]);
                        orderService.StartOrder(customers[i], tableNumber);
                        tableService.SetTableStatus(tableNumber);
                        break;
                    case MenuChoose.FreeTable:
                        tableNumber = tableService.ChooseOccupierTable();
                        orderService.DeleteOrder(tableNumber);
                        tableService.SetTableStatus(tableNumber);
                        break;
                    case MenuChoose.ViewMenu:
                        foodMenu.ShowMenu();
                        drinkMenu.ShowMenu();
                        break;
                    case MenuChoose.PlaceOrder:
                        orderId = orderService.FindNotPaidOrder();
                        foodMenu.ChooseItem(orderId);
                        drinkMenu.ChooseItem(orderId);
                        orderService.CalculateTotal(orderId);
                        orderService.ShowAllOrder(orderId);
                        break;
                    case MenuChoose.PayOrder:
                        orderId = orderService.FindNotPaidOrder();
                        tableNumber = orderService.PayOrder(orderId);
                        tableService.SetTableStatus(tableNumber);
                        checksService.SaveChecks(orderId);
                        var task = emailService.SendEmail(orderId);

                        break;
                    case MenuChoose.Exit:
                        return;
                    default:
                        continue;
                }
                i = UpdateIndex();
            }


            int UpdateIndex()
            {
                i++;
                if (i >= customers.Length)
                {
                    i = 0;
                }
                return i;
            }
        }
    }
}