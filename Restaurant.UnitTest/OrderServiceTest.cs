using Restaurant.Models;
using Restaurant.Repositories;
using Restaurant.Services;

namespace Restaurant.UnitTest
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void CheckIsOrderInList_InputTableNumberAndOrderList_OutputBoolean()
        {
            //1. Arrange
            var userInterface = new UiService();
            var orderService = new OrdersService(userInterface);
            var orderRepository = new OrderRepository();
            var orderList = new List<Order>(orderRepository.GetOrdersList().Where(x => x.IsPaid == true));
            var tableNumber = 7;            
            
            //2. Act
            var result = orderService.CheckIsOrderInList(tableNumber, orderList);
            //3. Assert
            Assert.IsFalse(result);
        }
        public void GetOrderByTableName_InputTableNumberAndIsPaid_OutputOrder() 
        {
            //1. Arrange


        }


    }
}