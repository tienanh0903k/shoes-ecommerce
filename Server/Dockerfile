# ===== Stage 1: Build =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy toàn bộ source code
COPY . .

# Restore & publish project
RUN dotnet restore "./ShoesStoreApp.PLA/ShoesStoreApp.PLA.csproj"
RUN dotnet publish "./ShoesStoreApp.PLA/ShoesStoreApp.PLA.csproj" -c Release -o /app/publish

# ===== Stage 2: Runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy file đã build từ stage build
COPY --from=build /app/publish .

# Cấu hình ASP.NET lắng nghe toàn bộ IP trên HTTP port 5179
ENV ASPNETCORE_URLS=http://+:5179

# Mở cổng 5179
EXPOSE 5179

# Command để chạy API
ENTRYPOINT ["dotnet", "ShoesStoreApp.PLA.dll"]
