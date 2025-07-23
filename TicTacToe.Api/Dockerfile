# Базовый образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080  # Порт, который слушает приложение

# Сборка приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TicTacToe.Api.csproj", "."]
RUN dotnet restore "TicTacToe.Api.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

# Публикация приложения
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.Api.dll"]