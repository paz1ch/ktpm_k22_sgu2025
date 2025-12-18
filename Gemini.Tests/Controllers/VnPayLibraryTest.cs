using System;
using NUnit.Framework;
using Gemini.Controllers.Bussiness;

namespace Gemini.Tests.Controllers.Bussiness
{
    [TestFixture]
    public class VnPayLibraryTest
    {
        private VnPayLibrary _vnPayLib;

        [SetUp]
        public void Setup()
        {
            _vnPayLib = new VnPayLibrary();
        }

        [Test]
        public void AddRequestData_ValidKeyAndValue_StoresData()
        {
            // Arrange
            string key = "vnp_Amount";
            string value = "100000";

            // Act
            _vnPayLib.AddRequestData(key, value);

            // Assert
            // Use reflection or verify CreateRequestUrl includes this data indirectly
            string baseUrl = "http://test.com";
            string secretKey = "secret";
            string url = _vnPayLib.CreateRequestUrl(baseUrl, secretKey);
            
            StringAssert.Contains("vnp_Amount=100000", url);
        }

        [Test]
        public void CreateRequestUrl_WithData_GeneratesValidChecksum()
        {
            // Arrange
            string baseUrl = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string secretKey = "NJLWXDHDIOCNGCDHTDHHTIAHYKZFVDTN"; // Sample secret
            
            _vnPayLib.AddRequestData("vnp_Version", "2.1.0");
            _vnPayLib.AddRequestData("vnp_Command", "pay");
            _vnPayLib.AddRequestData("vnp_TmnCode", "TESTCODE");
            _vnPayLib.AddRequestData("vnp_Amount", "1000000"); // 10,000 VND

            // Act
            string url = _vnPayLib.CreateRequestUrl(baseUrl, secretKey);

            // Assert
            Assert.IsNotNull(url);
            StringAssert.StartsWith(baseUrl + "?", url);
            StringAssert.Contains("vnp_Version=2.1.0", url);
            StringAssert.Contains("vnp_SecureHash=", url);
            
            // Verify checksum length (SHA512 hex is 128 chars)
            int hashIndex = url.IndexOf("vnp_SecureHash=");
            string hash = url.Substring(hashIndex + 15);
            Assert.AreEqual(128, hash.Length, "SHA512 hash should be 128 (hex) characters long.");
        }
    }
}
