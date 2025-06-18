FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./ShoesStoreApp.PLA/ShoesStoreApp.PLA.csproj"
RUN dotnet publish "./ShoesStoreApp.PLA/ShoesStoreApp.PLA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ShoesStoreApp.PLA.dll"]
EXPOSE 80
