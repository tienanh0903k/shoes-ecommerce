# Shoes E-Commerce

## Giới thiệu

Shoes E-Commerce là một ứng dụng web thương mại điện tử hoàn chỉnh (full-stack) được thiết kế để quản lý và bán các sản phẩm giày dép. Dự án này được xây dựng mạnh mẽ với Angular cho giao diện người dùng và ASP.NET Core cho phía máy chủ (backend), cùng với SQL Server làm hệ quản trị cơ sở dữ liệu.

Ứng dụng cung cấp các tính năng cốt lõi của một trang thương mại điện tử, bao gồm:

- **Quản lý danh mục và sản phẩm:** Dễ dàng thêm, sửa, xóa các loại giày và sản phẩm liên quan.
- **Giỏ hàng và quy trình thanh toán:** Trải nghiệm mua sắm mượt mà với giỏ hàng tiện lợi và quy trình thanh toán đơn giản.
- **Bảng điều khiển quản trị viên:** Một giao diện mạnh mẽ dành cho quản trị viên để theo dõi đơn hàng, quản lý sản phẩm, và nhiều tác vụ quản lý khác.

## Công nghệ sử dụng

Dự án sử dụng các công nghệ hiện đại và phổ biến:

- **Frontend:** Angular, HTML5, CSS3, TypeScript
- **Backend:** ASP.NET Core (API RESTful)
- **Database:** SQL Server
- **Xác thực:** JSON Web Tokens (JWT)
- **Triển khai:** Docker (sử dụng docker-compose để đóng gói và triển khai ứng dụng)
- **Giao tiếp:** Tích hợp CORS (Cross-Origin Resource Sharing) để cho phép tương tác an toàn giữa các domain khác nhau.

## Đã triển khai

Ứng dụng đã được triển khai thực tế và bạn có thể truy cập demo trực tiếp tại địa chỉ sau:

➡️ [Shoes E-Commerce Live Demo](http://banhangonline.vnmylove.top/)

## Cấu trúc dự án

Dự án được tổ chức một cách rõ ràng để dễ dàng bảo trì và phát triển:

```
/shoes-ecommerce
│
├── /Client             # Mã nguồn frontend (Angular)
│   └── ...
├── /Server             # Mã nguồn backend (ASP.NET Core)
│   └── ...
├── /docker-compose.yml # File cấu hình Docker Compose cho việc triển khai đa dịch vụ
├── /README.md          # Tài liệu mô tả dự án (file bạn đang đọc)
└── /LICENSE            # Giấy phép sử dụng dự án (MIT License)
```

## Cài đặt và chạy dự án

Để cài đặt và chạy dự án này trên máy cục bộ của bạn, vui lòng làm theo các bước dưới đây:

### Yêu cầu cài đặt trước

Đảm bảo bạn đã cài đặt các công cụ sau trên máy tính của mình:

- Node.js & npm: Phiên bản LTS được khuyến nghị.
- Angular CLI: Cài đặt toàn cầu bằng `npm install -g @angular/cli`.
- .NET SDK: Phiên bản tương thích với ASP.NET Core được sử dụng trong dự án (ví dụ: .NET 6.0 SDK hoặc mới hơn).
- SQL Server: Một instance của SQL Server (có thể là LocalDB, SQL Server Express, hoặc phiên bản đầy đủ).
- Git: Để sao chép mã nguồn từ repository.

### 1. Sao chép Repository

Mở Terminal hoặc Command Prompt và sao chép mã nguồn về máy của bạn:

```bash
git clone https://github.com/tienanh0903k/shoes-ecommerce.git
cd shoes-ecommerce
```

### 2. Cài đặt và chạy Backend (ASP.NET Core)

Điều hướng vào thư mục Server và cấu hình cơ sở dữ liệu:

```bash
cd Server
```

#### a. Cấu hình Cơ sở dữ liệu:

Mở file cấu hình ứng dụng (`appsettings.json` hoặc `appsettings.Development.json`) trong thư mục Server và cập nhật chuỗi kết nối (Connection String) cho SQL Server của bạn.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ShoesECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  }
  // ... các cấu hình khác
}
```

*Lưu ý:* Thay thế `YOUR_SERVER_NAME` bằng tên máy chủ SQL Server của bạn.

#### b. Áp dụng Migrations và Khởi tạo Database:

Chạy các lệnh sau để tạo và cập nhật cơ sở dữ liệu dựa trên các migrations đã định nghĩa:

```bash
dotnet ef database update
```

*(Nếu bạn gặp lỗi với `dotnet ef`, hãy đảm bảo bạn đã cài đặt công cụ EF Core toàn cầu: `dotnet tool install --global dotnet-ef`)*

#### c. Cài đặt Dependencies và Chạy Ứng dụng Backend:

```bash
dotnet restore
dotnet build
dotnet run
```

Backend sẽ chạy mặc định trên `https://localhost:7000` hoặc một cổng khác được cấu hình.

### 3. Cài đặt và chạy Frontend (Angular)

Mở một Terminal hoặc Command Prompt mới, điều hướng về thư mục gốc của dự án và sau đó vào thư mục Client:

```bash
cd .. # Quay lại thư mục gốc shoes-ecommerce
cd Client
```

#### a. Cài đặt Dependencies:

```bash
npm install
```

#### b. Chạy Ứng dụng Frontend:

```bash
ng serve --open
```

Ứng dụng frontend sẽ tự động mở trong trình duyệt của bạn tại `http://localhost:4200/`.

*Lưu ý:* Nếu backend chạy trên một cổng khác (`https://localhost:7000`), bạn cần cập nhật URL API trong môi trường Angular (ví dụ: `src/environments/environment.ts` và `src/environments/environment.prod.ts`) để frontend có thể giao tiếp với backend một cách chính xác.

## Tính năng nổi bật

- **Trang chủ và Danh sách sản phẩm:** Hiển thị các sản phẩm giày với hình ảnh, mô tả, và giá cả.
- **Tìm kiếm và Lọc sản phẩm:** Dễ dàng tìm kiếm sản phẩm theo tên, danh mục hoặc các tiêu chí khác.
- **Chi tiết sản phẩm:** Xem thông tin chi tiết về từng sản phẩm, bao gồm các biến thể (kích thước, màu sắc).
- **Giỏ hàng:** Thêm, xóa, cập nhật số lượng sản phẩm trong giỏ hàng.
- **Thanh toán:** Quy trình thanh toán đơn giản và an toàn.
- **Quản lý tài khoản người dùng:** Đăng ký, đăng nhập, quản lý thông tin cá nhân.
- **Bảng điều khiển quản trị viên:**
  - Quản lý người dùng, sản phẩm, danh mục.
  - Theo dõi và xử lý đơn hàng.
  - Thống kê và báo cáo cơ bản.
- **API RESTful:** Backend cung cấp các API mạnh mẽ và bảo mật để frontend tương tác.
- **Đăng nhập/Đăng ký:** Hệ thống xác thực người dùng dựa trên JWT.

## Đóng góp

Chúng tôi rất hoan nghênh những đóng góp của bạn để làm cho dự án này trở nên tốt hơn!

Để đóng góp, vui lòng thực hiện theo các bước sau:

1. Fork repository này.
2. Tạo một nhánh mới cho tính năng hoặc sửa lỗi của bạn:

   ```bash
   git checkout -b feature/your-feature-name
   ```

   *(Ví dụ: `git checkout -b feature/add-product-search`)*

3. Thực hiện các thay đổi của bạn và commit chúng với thông điệp rõ ràng:

   ```bash
   git commit -am 'Feat: Add new product search functionality'
   ```

4. Push nhánh của bạn lên repository đã fork:

   ```bash
   git push origin feature/your-feature-name
   ```

5. Mở một Pull Request (PR) tới nhánh main của repository gốc. Vui lòng cung cấp mô tả chi tiết về thay đổi của bạn.

## Giấy phép

Dự án này được cấp phép theo giấy phép MIT. Bạn có thể tìm thấy chi tiết đầy đủ trong tệp LICENSE tại thư mục gốc của dự án.
