├── src/
│   ├── Core/                           # Các thành phần cốt lõi (Domain)
│   │   ├── Entities/                   # Các entity chính của ứng dụng
│   │   ├── Interfaces/                 # Các interface chung (service, repository)
│   │   ├── ValueObjects/               # Các đối tượng giá trị (Value Objects)
│   │   ├── Exceptions/                 # Các ngoại lệ tuỳ chỉnh
│   │   └── Validators/                 # Xác thực dữ liệu, validation logic
│   ├── Application/                    # Logic nghiệp vụ của ứng dụng (Use Cases)
│   │   ├── DTOs/                       # Các Data Transfer Object (DTOs) cho API
│   │   ├── Interfaces/                 # Các interface cho services (application layer)
│   │   ├── Services/                   # Các lớp xử lý nghiệp vụ
│   │   └── Mappings/                   # Các lớp chuyển đổi giữa entities và DTOs
│   ├── Infrastructure/                 # Thành phần hạ tầng (Database, Services bên ngoài)
│   │   ├── Data/                       # Lớp kết nối cơ sở dữ liệu (DbContext, migrations)
│   │   ├── Repositories/               # Các repository thực hiện CRUD trên entity
│   │   ├── Services/                   # Các service ngoài như email, gửi thông báo, v.v.
│   │   └── DependencyInjection/        # Đăng ký các dịch vụ vào DI container
│   ├── WebApi/                         # API layer (entry point cho ứng dụng)
│   │   ├── Controllers/                # Các controllers xử lý các HTTP request
│   │   ├── Middlewares/                # Các middleware như xử lý lỗi, logging, auth
│   │   ├── Program.cs                  # Cấu hình ứng dụng
│   │   ├── Startup.cs                  # Đăng ký các dịch vụ, middleware
│   │   └── appsettings.json            # Cấu hình môi trường chung
├── tests/                              # Kiểm thử ứng dụng (Unit Tests, Integration Tests)
│   ├── Core.Tests/                     # Kiểm thử cho các lớp trong Core
│   ├── Application.Tests/              # Kiểm thử cho các dịch vụ nghiệp vụ trong Application
│   ├── Infrastructure.Tests/           # Kiểm thử cho các lớp trong Infrastructure
│   └── WebApi.Tests/                   # Kiểm thử cho các controller API
└── README.md                           # Tài liệu mô tả về dự án
