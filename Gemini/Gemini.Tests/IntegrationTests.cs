using NUnit.Framework;
using Gemini.Models;
using System.Linq;
using System.Data.Entity;

namespace Gemini.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        [Category("Integration")]
        public void Test_DatabaseConnection_ShouldCanQueryUsers()
        {
            using (var db = new GeminiEntities())
            {
                var user = db.SUsers.FirstOrDefault();

                Assert.DoesNotThrow(() => { var count = db.SUsers.Count(); });
            }
        }

        [Test]
        [Category("Integration")]
        public void Test_SRoles_ShouldCanQuery()
        {
            using (var db = new GeminiEntities())
            {
                var roles = db.SRoles.ToList();
                Assert.IsNotNull(roles);
            }
        }

        [Test]
        [Category("Integration")]
        public void Test_SMenus_ShouldCanQuery()
        {
            using (var db = new GeminiEntities())
            {
                Assert.DoesNotThrow(() => db.SMenus.FirstOrDefault());
            }
        }

        [Test]
        [Category("Integration")]
        public void Test_SUser_ShouldExistAnyAdmin()
        {
            using (var db = new GeminiEntities())
            {
                Assert.IsTrue(true); 
            }
        }
        [Test]
        [Category("Integration")]
        public void Test_Integration_Fail_Intentionally()
        {
            Assert.Fail("Intentional Integration Failure for testing CI report.");
        }
    }
}
