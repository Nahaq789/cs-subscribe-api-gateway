FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app/src/ApiGateway
COPY ["src/ApiGateway/ApiGateway.csproj", "./"]
RUN dotnet restore "ApiGateway.csproj"
COPY src/ApiGateway .
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UserAppHosts=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "ApiGateway.dll" ]