
using Restaurant.Services;

namespace Restaurant.UnitTest
{
    [TestClass]
    public class EmailServiceTest
    {
        [TestMethod]
        public void EmailIsValid_InputValidEmail_OurputBoolenTrue() 
        {
            //1. Arrange
            var email = "test@test.txt";            
            var emailService = new EmailService();
            //2. Act
            var result = emailService.EmailIsValid(email);
            //3. Assert            
            Assert.IsTrue(result);
        }
    }
}
