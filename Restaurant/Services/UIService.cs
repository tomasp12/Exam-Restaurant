using Restaurant.Enum;
using Restaurant.Interfaces;
using Restaurant.Models;

namespace Restaurant.Services
{
    public class UiService
    {
        public MenuChoose ShowMainMenu()
        {
            do
            {
                CleanScreen();
                OutputText(@$"1. Choose Table\n
                              2. Free table\n   
                              3. View menu \n
                              4. Place order\n                          
                              5. Pay order\n                          
                            ");
                var choice = AskChoose(@"\n Enter your choose (0 exit):");
                switch (choice)
                {
                    case 1:
                        return MenuChoose.ChooseTable;
                    case 2:
                        return MenuChoose.FreeTable;
                    case 3:
                        return MenuChoose.ViewMenu;
                    case 4:
                        return MenuChoose.PlaceOrder;
                    case 5:
                        return MenuChoose.PayOrder;
                    case 0:
                        return MenuChoose.Exit;
                    default:
                        continue;
                }
            } while (true);
        }

        public void OutputText(string text)
        {
            foreach (var value in text.Split(@"\n"))
            {
                Console.WriteLine(value.TrimStart());
            }
        }

        private void CleanScreen()
        {
            Console.Clear();
        }

        public void PauseOutput()
        {
            Console.ReadKey();
        }

        public void OutputContinueText()
        {
            OutputText(@"\n \n Any key to continue...");
            PauseOutput();
        }

        private void OutputErrorText()
        {
            OutputText("Input Error");
            PauseOutput();
        }
        public int AskChoose(string text)
        {

            if (text != "")
            {
                OutputText(text);
            }
            int input = 0;
            bool success = false;
            while (!success)
            {                   
                success = int.TryParse(Console.ReadLine(), out input);
                if (!success)
                {
                    OutputText("Invalid input. Please try again.");
                }
            }   
            return input;
        }

        public int ShowTableChoose(int customersNumber, int tableNumber)
        {

            OutputText(@$"\n For {customersNumber} person\n we suggest table number {tableNumber},\n enter yours choose (0 exit):");
            return AskChoose("");

        }
        public void ShowPersonNumber(int customersNumber)
        {

            CleanScreen();
            OutputText($"{customersNumber} people came.");
            OutputContinueText();

        }
        public void ShowTableList(List<Table> tablesList)
        {
            CleanScreen();
            OutputText($"Tables List");
            foreach (var table in tablesList)
            {
                var occupier = table.IsOccupied ? "occupier" : "free";
                OutputText($"{table.TableNumber} table have {table.Seats} seats and is {occupier}");
            }

        }
        public void ShowOrdersList(List<Order> orderList)
        {
            CleanScreen();
            OutputText("Orders List");
            foreach (var order in orderList)
            {
                var paid = order.IsPaid ? "paid" : "not paid";
                OutputText($"{order.TableNumber} table have {order.CustomersNumber} customers, {paid}");
            }
        }


        public void ShowPayMenu(Order order)
        {
            OutputText($"Yours total {order.TotalCost:#.##}.");
            PauseOutput();
        }


        public void ShowMenuList<T>(List<T> menuList) where T : class, IMenuItems
        {
            CleanScreen();
            OutputText($"{typeof(T).Name} Menu:");
            menuList.ForEach(x => OutputText($"{x.Id}. {x.Price} {x.Name} "));
            //OutputContinueText();
        }

        public void OutputMessage(ErrorMessage messege)
        {
            switch (messege)
            {
                case ErrorMessage.Order:
                    CleanScreen();
                    OutputText("No orders");
                    PauseOutput();
                    break;
                case ErrorMessage.Table:
                    CleanScreen();
                    OutputText("No table found");
                    PauseOutput();
                    break;
                case ErrorMessage.BusyTable:
                    OutputText("Table occupied");
                    PauseOutput();
                    break;
                case ErrorMessage.Email:
                    OutputText("Wrong email");
                    PauseOutput();
                    break;

                default:
                    return;
            }
        }

        public void ShowOrderInfo(Order order, List<OrderItem> orderItems)
        {
            CleanScreen();
            OutputText($@"OrderNr: {order.OrderId}\n
                          Order data: {order.OrderDate: yy/MM/dd H:mm}\n
                          {order.CustomersNumber} customers,\n
                          {order.TableNumber} table\n 
                          Meals:");
            orderItems.ForEach(x => OutputText($"  {x.Name} {x.Price}"));
            OutputText($"         Total EUR: {order.TotalCost:#.##}");
            OutputContinueText();
        }

        public string AskEmail()
        {
            while (true)
            {

                string? email = "";
                OutputText("Enter Email (empty no email send):");
                email = Console.ReadLine();
                if (email == "")
                {
                    return "";
                }
                var emailService = new EmailService();
                if (emailService.EmailIsValid(email))
                {
                    return email;
                }
                OutputMessage(ErrorMessage.Email);
            }
        }

    }
}
