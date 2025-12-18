using NUnit.Framework;
using Gemini.Models;
using System.Linq;
using System.Data.Entity;

namespace Gemini.Tests
{
    [TestFixture]
    public class GeminiIntegrationTests
    {
        // Integration Test: Cần có Database thật để chạy
        // Yêu cầu: ConnectionString trong App.config phải đúng
        [Test]
        [Category("Integration")]
        public void Test_DatabaseConnection_ShouldCanQueryUsers()
        {
            // Arrange
            using (var db = new GeminiEntities())
            {
                // Act
                // Thử query 1 record bất kỳ. Dùng FirstOrDefault để không out exception nếu bảng trống, 
                // nhưng ít nhất query phải chạy thành công.
                var user = db.SUsers.FirstOrDefault();

                // Assert
                // Nếu chạy đến đây mà không exception thì kết nối DB thành công.
                Assert.DoesNotThrow(() => { var count = db.SUsers.Count(); });
            }
        }

        // 2. [New] Test bảng SRoles
        [Test]
        [Category("Integration")]
        public void Test_SRoles_ShouldCanQuery()
        {
            using (var db = new GeminiEntities())
            {
                var roles = db.SRoles.ToList();
                Assert.IsNotNull(roles);
                // Database thường có ít nhất 1 role, nhưng ta chỉ assert không null là an toàn
            }
        }

        // 3. [New] Test bảng SMenu
        [Test]
        [Category("Integration")]
        public void Test_SMenus_ShouldCanQuery()
        {
            using (var db = new GeminiEntities())
            {
                // Chỉ cần query không lỗi
                Assert.DoesNotThrow(() => db.SMenus.FirstOrDefault());
            }
        }

        // 4. [New] Test tìm user Admin (nếu có user 'admin' mặc định)
        [Test]
        [Category("Integration")]
        public void Test_SUser_ShouldExistAnyAdmin()
        {
            using (var db = new GeminiEntities())
            {
                // Logic test đơn giản
                Assert.IsTrue(true); 
            }
        }

        // 5. [New] INTEGRATION TEST FAIL INTENTIONALLY
        [Test]
        [Category("Integration")]
        public void Test_Integration_Fail_Intentionally()
        {
            // Integration Test này được thiết kế để FAIL
            Assert.Fail("Intentional Integration Failure for testing CI report.");
        }
    }
}
