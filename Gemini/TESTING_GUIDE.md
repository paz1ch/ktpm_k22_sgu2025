
# Hướng dẫn Unit Test cho Dự án Gemini

Mình đã thiết lập sẵn môi trường Unit Test cho bạn bằng **NUnit** và **.NET SDK**.
Project test nằm tại thư mục: `Gemini.Tests`.

## 1. Cấu trúc Project
*   **Gemini (Main Project)**: Code chính của web.
*   **Gemini.Tests**: Chứa các file test (đã được add vào Solution).

## 2. Cách chạy Test
### Dùng Visual Studio
1.  Mở `Gemini.sln` (Reload nếu cần).
2.  Mở Menu **Test** -> **Run All Tests**.
3.  Xem kết quả ở cửa sổ **Test Explorer**.

### Dùng dòng lệnh (Terminal)
Chạy lệnh sau tại thư mục gốc:
```powershell
dotnet test Gemini.Tests\Gemini.Tests.csproj
```

## 3. Cách viết Test mới
Tạo một class mới trong `Gemini.Tests`, ví dụ `CalculatorTests.cs`:

```csharp
using NUnit.Framework;
using Gemini.Controllers; // Reference project chính

namespace Gemini.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Test_Addition()
        {
            int a = 5;
            int b = 10;
            Assert.AreEqual(15, a + b);
        }
    }
}
```

## 4. Lưu ý quan trọng (Mocking)
Hiện tại các Controller đang gắn chặt với Database (`GeminiEntities`) và `HttpContext`. Để test sâu hơn (ví dụ test `WOrderController`), bạn cần áp dụng kỹ thuật **Dependency Injection** hoặc dùng thư viện **Moq** để giả lập các đối tượng này, tránh gọi trực tiếp vào Database thật khi chạy test.
