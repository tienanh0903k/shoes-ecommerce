// ... các using ở đầu file ...

var builder = WebApplication.CreateBuilder(args);

// ===============================
// ĐĂNG KÝ CÁC SERVICE (builder.Services.Add... )
// TẤT CẢ CÁC builder.Services.Add... PHẢI NẰM Ở ĐÂY,
// TRƯỚC DÒNG `var app = builder.Build();`
// ===============================

// Payment
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ShoesStoreAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICartItemService, CartItemService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ISizeSrevice, SizeService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IUserService, UserService>(); // Thêm nếu cần ShoesStoreApp.BLL.Services.CustomerService

// ====================================================================
// CẤU HÌNH CORS POLICY (ĐÚNG VỊ TRÍ!)
// PHẢI NẰM TRONG KHỐI builder.Services.Add...
// TRƯỚC DÒNG `var app = builder.Build();`
// ====================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", // Tên chính sách bạn sẽ dùng trong app.UseCors
        policyBuilder =>
        {
            policyBuilder.WithOrigins(
                                      "http://tientienh2003mylove.top", // Domain bạn đang dùng
                                      "http://your_vps_ip",            // Địa chỉ IP của VPS nếu bạn truy cập bằng IP (thay your_vps_ip bằng IP thật)
                                      "http://your_vps_ip:80",         // Nếu frontend chạy trên cổng 80 của VPS
                                      "http://your_vps_ip:8080",       // Nếu frontend chạy trên cổng 8080 của VPS
                                      "http://localhost:8080",         // Nếu bạn test frontend cục bộ
                                      "http://localhost:4200"          // Nếu bạn test frontend Angular cục bộ
                                     )
                       .AllowAnyHeader()
                       .AllowAnyMethod();
        });
});
// ====================================================================

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ShoesStoreAppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"].ToString())),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"]
        };
    });

// ====================================================================
// ĐOẠN NÀY ĐÁNH DẤU KẾT THÚC PHẦN `builder.Services.Add...`
// KHÔNG THÊM BẤT KỲ builder.Services.Add... NÀO SAU DÒNG NÀY
// ====================================================================
var app = builder.Build();

// ====================================================================
// CẤU HÌNH HTTP REQUEST PIPELINE (app.Use...)
// ====================================================================

// Phần Seed Role (Nếu không cần, hãy để comment hoặc xóa)
// static async Task SeedRolesAsync(RoleManager<Role> roleManager) { ... }
// using (var scope = app.Services.CreateScope()) { ... }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// QUAN TRỌNG: UseRouting PHẢI NẰM TRƯỚC UseCors
app.UseRouting();

// KÍCH HOẠT CORS MIDDLEWARE
app.UseCors("MyCorsPolicy"); // Sử dụng tên chính sách bạn đã định nghĩa ở trên

app.UseAuthentication();
app.UseAuthorization();

// Cấu hình Static Files (cho ảnh)
var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();