using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Restaurant.Repositories;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public class EmailService
    {
        private readonly UiService _ui;
        private readonly OrderRepository _orderRepository;
        private readonly OrderItemRepository _orderItemRepository;
        public EmailService()
        {   
            _ui= new UiService();
            _orderRepository = new OrderRepository();
            _orderItemRepository = new OrderItemRepository();
        }
        public bool EmailIsValid(string email)
        {
            var valid = true;
            try
            {
                var emailAddress = new MailAddress(email);
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
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("restaurant@gmail.com");
                    message.To.Add($"{order.CustomerEmail}");
                    message.Subject = "Check from restaurant";
                    var body = LoadCheck(order.OrderId.ToString());
                    message.Body = body;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
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
        
        
    }
}
