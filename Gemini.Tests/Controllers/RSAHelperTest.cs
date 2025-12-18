using System;
using NUnit.Framework;
using Gemini.Controllers.Bussiness;

namespace Gemini.Tests.Controllers.Bussiness
{
    [TestFixture]
    public class RSAHelperTest
    {
        private RSAHelper _rsaHelper;

        [SetUp]
        public void Setup()
        {
            _rsaHelper = new RSAHelper();
        }

        [Test]
        public void ConvertStringToHash_InputString_ReturnsHash()
        {
            // Arrange
            string input = "123456";
            
            // Act
            string hash = _rsaHelper.ConvertStringToHash(input);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsNotEmpty(hash);
            Assert.AreNotEqual(input, hash);
            // Verify deterministic result (same input -> same hash)
            string hash2 = _rsaHelper.ConvertStringToHash(input);
            Assert.AreEqual(hash, hash2);
        }

        [Test]
        public void SignAndVerifyData_ValidFlow_ReturnsTrue()
        {
            // Arrange
            string originalMessage = "TransactionData";
            string privateKey;
            string publicKey;
            string keyContainer = "TestContainer";

            // 1. Generate Keys
            _rsaHelper.GenerateKeys(keyContainer, out privateKey, out publicKey);

            // Act
            // 2. Sign Data with Private Key
            string signedMessage = _rsaHelper.SignData(originalMessage, privateKey);

            // 3. Verify Data with Public Key
            bool isVerified = _rsaHelper.VerifyData(originalMessage, signedMessage, publicKey);

            // Assert
            Assert.IsNotNull(signedMessage);
            Assert.IsTrue(isVerified, "Verification should pass with correct keys and message.");
        }

        [Test]
        public void VerifyData_TamperedMessage_ReturnsFalse()
        {
            // Arrange
            string originalMessage = "OriginalData";
            string tamperedMessage = "TamperedData";
            string privateKey;
            string publicKey;
            string keyContainer = "TestContainer_Tamper";

            _rsaHelper.GenerateKeys(keyContainer, out privateKey, out publicKey);
            string signedMessage = _rsaHelper.SignData(originalMessage, privateKey);

            // Act
            bool isVerified = _rsaHelper.VerifyData(tamperedMessage, signedMessage, publicKey);

            // Assert
            Assert.IsFalse(isVerified, "Verification should fail when message is tampered.");
        }
    }
}
