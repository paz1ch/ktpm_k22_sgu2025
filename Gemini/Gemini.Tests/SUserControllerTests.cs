using Gemini.Controllers._01_Hethong;
using NUnit.Framework;
using System.IO;

namespace Gemini.Tests
{
    [TestFixture]
    public class SUserControllerTests
    {
        // Module: 01_Hethong/SUserController
        // Chức năng: Quản lý User
        // Hàm test: Các hàm xử lý file/thư mục (Static Methods)

        [Test]
        public void Test_VerifyDir_ShouldCreateDirectory_IfNotExist()
        {
            // Arrange
            // VerifyDir strips the last part (filename) and creates the parent directory.
            string targetDir = Path.Combine(Path.GetTempPath(), "GeminiTestDir_" + System.Guid.NewGuid());
            string dummyFilePath = Path.Combine(targetDir, "dummy.txt");

            if (Directory.Exists(targetDir))
            {
                Directory.Delete(targetDir, true);
            }

            // Act
            // Pass the full file path. VerifyDir should create 'targetDir'.
            SUserController.VerifyDir(dummyFilePath);

            // Assert
            Assert.IsTrue(Directory.Exists(targetDir));

            // Cleanup
            if (Directory.Exists(targetDir))
            {
                Directory.Delete(targetDir, true);
            }
        }

        [Test]
        public void Test_WriteFileFromStream_ShouldWriteFileCorrectly()
        {
            // Arrange
            string content = "Hello Gemini";
            string tempFile = Path.Combine(Path.GetTempPath(), "GeminiTestFile_" + System.Guid.NewGuid() + ".txt");
            
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
            {
                // Act
                SUserController.WriteFileFromStream(stream, tempFile);
            }

            // Assert
            Assert.IsTrue(File.Exists(tempFile));
            string readContent = File.ReadAllText(tempFile);
            Assert.AreEqual(content, readContent);

            // Cleanup
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
