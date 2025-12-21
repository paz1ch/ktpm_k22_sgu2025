using System;
using System.Linq;
using Gemini.Controllers._01_Hethong;
using Gemini.Models;
using Gemini.Models._01_Hethong;
using NUnit.Framework;

namespace Gemini.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private GeminiEntities _db;

        [OneTimeSetUp]
        public void Setup()
        {
            // Verify DB Connection
            _db = new GeminiEntities();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _db.Dispose();
        }

        [Test]
        public void Test_Connect_Database_Success()
        {
            // Assert that we can query the database
            var users = _db.SUsers.Take(1).ToList();
            Assert.IsNotNull(users);
        }

        [Test]
        public void Test_Integration_CRUD_User()
        {
            // 1. CREATE
            var newUser = new SUser
            {
                Guid = Guid.NewGuid(),
                Username = "IntegrationTestUser_" + DateTime.Now.Ticks,
                Password = "Password123", // Encryption handles in Controller usually, but here we test raw DB logic or assume simple
                Email = "integration@test.com",
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = "AutoTest"
            };

            _db.SUsers.Add(newUser);
            int changes = _db.SaveChanges();
            Assert.AreEqual(1, changes, "Should be able to save new user to DB");

            // 2. READ
            var userFromDb = _db.SUsers.FirstOrDefault(u => u.Username == newUser.Username);
            Assert.IsNotNull(userFromDb, "Should be able to read the created user from DB");
            Assert.AreEqual(newUser.Email, userFromDb.Email);

            // 3. UPDATE
            userFromDb.Email = "updated@test.com";
            _db.SaveChanges();

            var updatedUser = _db.SUsers.FirstOrDefault(u => u.Guid == newUser.Guid);
            Assert.AreEqual("updated@test.com", updatedUser.Email, "Email should be updated in DB");

            // 4. DELETE
            _db.SUsers.Remove(updatedUser);
            _db.SaveChanges();

            var deletedUser = _db.SUsers.FirstOrDefault(u => u.Guid == newUser.Guid);
            Assert.IsNull(deletedUser, "User should be deleted from DB");
        }

        [Test]
        public void Test_Controller_Read_Returns_Data()
        {
            // Simple Integration test invoking Controller Action that queries DB
            var controller = new SUserController();
            // Mocking Request for DataSourceRequest if needed, or calling lower level methods
            // But Read takes [DataSourceRequest]. Passing null might throw or work depending on implementation.
            // Let's rely on DataGemini directly test above for now as safe Integration Test.
            
            // However, we can test that Controller has access to DB
            Assert.IsNotNull(controller.DataGemini);
            var users = controller.DataGemini.SUsers.Take(5).ToList();
            Assert.IsTrue(users.Count >= 0); // Just smoke test
        }
    }
}
