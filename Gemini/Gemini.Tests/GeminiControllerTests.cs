using NUnit.Framework;
using Gemini.Controllers.Bussiness;
using System.Linq;

namespace Gemini.Tests
{
    [TestFixture]
    public class GeminiControllerTests
    {
        /*
        // 1. Test cho hàm tiện ích (Utility Method) - Không dính DB/Session
        [Test]
        public void Test_SplitStringToArr_ShouldSplitCorrectly()
        {
            // Arrange (Chuẩn bị)
            var controller = new GeminiController();
            string input = "apple,banana,orange";
            char separator = ',';

            // Act (Thực hiện)
            string[] result = controller.SplitStringToArr(input, separator);

            // Assert (Kiểm tra kết quả)
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual("apple", result[0]);
            Assert.AreEqual("banana", result[1]);
            Assert.AreEqual("orange", result[2]);
        }
        */

        // 2. Test cho hàm Static (Encrypt/Decrypt)
        [Test]
        public void Test_Encrypt_Decrypt_ShouldReturnOriginalString()
        {
            // Arrange
            string originalText = "SecretPassword123";

            // Act
            string encrypted = GeminiController.Encrypt(originalText);
            string decrypted = GeminiController.Decrypt(encrypted);

            // Assert
            Assert.AreNotEqual(originalText, encrypted); // Mã hóa phải khác bản gốc
            Assert.AreEqual(originalText, decrypted);   // Giải mã phải về lại bản gốc
        }

        /*
        // 3. Test trường hợp chuỗi rỗng
        [Test]
        public void Test_SplitStringToArr_EmptyString()
        {
            var controller = new GeminiController();
            var result = controller.SplitStringToArr("", ',');
            
            // Hàm Split của C# với chuỗi rỗng trả về mảng 1 phần tử là chuỗi rỗng
            Assert.AreEqual(1, result.Length); 
            Assert.AreEqual("", result[0]);
        }
        */
    }
}
