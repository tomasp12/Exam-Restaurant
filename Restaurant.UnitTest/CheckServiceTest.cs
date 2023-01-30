using Restaurant.Models;
using Restaurant.Repositories;
using Restaurant.Services;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.UnitTest
{
    [TestClass]
    public class CheckServiceTest
    {
        [TestMethod]
        public void WriteTextToFile_InputPathANdString_OutputChekIsFileCreated() 
        {
            //1. Arrange
            var path = "test.txt";
            var text = "test string";
            var checkService = new ChecksService(new UiService());
            //2. Act
            var task =checkService.WriteTextToFile (path, text);
            //3. Assert
            var result = File.Exists(path);
            Assert.IsTrue(result);
        }
        
    }
}
