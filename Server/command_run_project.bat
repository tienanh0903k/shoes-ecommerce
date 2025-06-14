@echo off
REM Di chuyển đến đúng thư mục nếu cần (ví dụ: D:\Documents\dotNET\ShoeStore\Server)
REM cd /d D:\Documents\dotNET\ShoeStore\Server

dotnet restore ShoesStoreApp.Server.sln

dotnet ef migrations add Init --project ./ShoesStoreApp.DAL --startup-project ./ShoesStoreApp.PLA
dotnet ef database update --project ./ShoesStoreApp.DAL --startup-project ./ShoesStoreApp.PLA

REM dotnet ef migrations remove --project ./ShoesStoreApp.DAL --startup-project ./ShoesStoreApp.PLA



pause
