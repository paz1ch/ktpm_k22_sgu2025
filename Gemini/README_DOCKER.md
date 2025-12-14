# Hướng dẫn chạy Gemini Project với Docker

Tài liệu này hướng dẫn cách build và chạy ứng dụng Gemini (ASP.NET MVC 5) sử dụng Docker trên Windows.

## 1. Yêu cầu hệ thống (Prerequisites)

*   **Hệ điều hành**: Windows 10 Pro/Enterprise hoặc Windows 11.
*   **Docker Desktop**: Đã cài đặt.
*   **Chế độ Container**: Phải chuyển sang **Windows Containers**.
    *   Click chuột phải vào biểu tượng Docker ở system tray -> chọn "Switch to Windows containers...".

## 2. Cấu hình Cơ sở dữ liệu (Database)

Vì ứng dụng chạy trên Windows Containers, việc chạy SQL Server (thường là Linux container) cùng lúc có thể gặp khó khăn tùy thuộc vào cấu hình Docker của bạn. Bạn có 2 lựa chọn:

### Lựa chọn A: Sử dụng SQL Server trên máy thật (Khuyên dùng)
Đây là cách ổn định nhất. Ứng dụng trong Docker sẽ kết nối tới SQL Server đang chạy trên máy Windows của bạn.

1.  Mở file `docker-compose.yml`.
2.  Comment hoặc xóa service `db`.
3.  Sửa biến môi trường trong service `gemini-web`:
    *   `DB_SERVER=host.docker.internal` (Đây là địa chỉ trỏ về máy thật).
    *   `DB_USER`: Tên đăng nhập SQL của bạn (ví dụ: `sa`).
    *   `DB_PASS`: Mật khẩu SQL của bạn.
4.  Đảm bảo SQL Server trên máy bạn đã bật **TCP/IP** và cho phép kết nối từ xa (Allow Remote Connections).

### Lựa chọn B: Sử dụng SQL Server Container
Nếu bạn muốn chạy SQL Server trong Docker (như cấu hình hiện tại trong `docker-compose.yml`), bạn cần đảm bảo Docker Desktop hỗ trợ chạy mixed containers hoặc bạn đang sử dụng Windows Server image cho SQL (rất nặng).

*   *Lưu ý*: File `docker-compose.yml` hiện tại đang dùng image `mssql/server:2019-latest` (Linux). Nếu bạn đang ở chế độ Windows Containers, service này có thể không chạy được trừ khi bạn dùng WSL2 backend.

## 3. Cách chạy ứng dụng

### Bước 1: Build và Run
Mở PowerShell hoặc Command Prompt tại thư mục dự án (`d:\GitHub\ktpm\Gemini`) và chạy:

```powershell
docker-compose up --build
```

*   Lệnh này sẽ build image `gemini-web` và khởi động các containers.
*   Quá trình build lần đầu có thể lâu do phải tải image Windows Server Core (~5GB).

### Bước 2: Truy cập ứng dụng
Sau khi khởi động thành công, truy cập trình duyệt tại:

*   **http://localhost:8080**

## 4. Troubleshooting (Sự cố thường gặp)

### Lỗi kết nối Database
*   **Triệu chứng**: Ứng dụng báo lỗi không kết nối được DB hoặc `entrypoint.ps1` báo lỗi.
*   **Khắc phục**:
    *   Kiểm tra lại `DB_SERVER`, `DB_USER`, `DB_PASS` trong `docker-compose.yml`.
    *   Nếu dùng `host.docker.internal`, hãy tắt Firewall hoặc tạo rule cho port 1433.

### Lỗi "image operating system 'linux' cannot be used on this platform"
*   **Nguyên nhân**: Bạn đang ở chế độ Windows Containers nhưng cố chạy image Linux (SQL Server).
*   **Khắc phục**: Chuyển sang **Lựa chọn A** (dùng DB máy thật) hoặc tìm image SQL Server phiên bản Windows (không khuyến khích do kích thước lớn).

### Lỗi Build
*   Đảm bảo bạn đã tắt Visual Studio hoặc các tiến trình đang giữ file trong thư mục `bin` hoặc `obj` trước khi build.
