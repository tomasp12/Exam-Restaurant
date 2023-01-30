using Restaurant.Repositories;
using System.Net;
using System.Net.Mail;


namespace Restaurant.Services
{
    public class EmailService
    {
        private readonly UiService _ui;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly ChecksService _checksService;
        public EmailService(UiService ui)
        {   
            _ui= ui;
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
            _checksService = new ChecksService(_ui);
        }
        public bool EmailIsValid(string? email)
        {
            var valid = true;
            try
            {
                var emailAddress = new MailAddress(email!);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        public async Task SendEmail(int orderId)
        {
            if (orderId == 0)
            {
                return;
            }
            var order = _orderRepository.GetOrderById(orderId);
            if (order.SendEmail)
            {
                try
                {
                    MailMessage message = new();
                    message.From = new("restaurant@gmail.com");
                    message.To.Add($"{order.CustomerEmail}");
                    message.Subject = "Check from restaurant";

                    var body = _checksService.LoadCheck(order.OrderId.ToString());
                    message.Body = body;
                    SmtpClient client = new("smtp.gmail.com", 587);
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("tomyte", "ulssfwsjruueezbv");
                    client.EnableSsl = true;
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    _ui.OutputText("Error sending email: " + ex.Message);
                    _ui.PauseOutput();
                }
            }
        }
    }
}
