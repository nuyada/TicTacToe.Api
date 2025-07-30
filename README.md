# TicTacToe API

Современное RESTful API для игры "Крестики-нолики" с поддержкой настраиваемого размера поля и условий победы.

## 🎯 Особенности

- **Настраиваемый размер поля**: Поддержка игровых полей различных размеров (3x3, 5x5 и т.д.)
- **Гибкие условия победы**: Настраиваемая длина выигрышной последовательности
- **Идемпотентность ходов**: Защита от дублирования ходов с использованием ETag
- **Обработка ошибок**: Глобальная обработка исключений с информативными сообщениями
- **База данных**: Персистентное хранение игр и ходов в PostgreSQL
- **Документация API**: Интеграция со Swagger/OpenAPI
- **Контейнеризация**: Готовая конфигурация Docker и Docker Compose
- **Тестирование**: Покрытие unit-тестами с использованием xUnit и Moq

## 🛠 Технологический стек

- **.NET 9.0**: Современная платформа разработки
- **ASP.NET Core Web API**: Фреймворк для создания REST API
- **Entity Framework Core**: ORM для работы с базой данных
- **PostgreSQL**: Реляционная база данных
- **Docker**: Контейнеризация приложения
- **Swagger/OpenAPI**: Документация API
- **xUnit + Moq**: Фреймворки для тестирования

## 🚀 Быстрый старт

### Предварительные требования

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker](https://www.docker.com/get-started) (для запуска с базой данных)

### Запуск с Docker Compose

```bash
# Клонирование репозитория
git clone <repository-url>
cd TicTacToe.Api

# Запуск приложения и базы данных
docker-compose up --build
```

API будет доступно по адресу: `http://localhost:8080`

### Локальный запуск

```bash
# Установка зависимостей
dotnet restore

# Запуск миграций базы данных
dotnet ef database update

# Запуск приложения
dotnet run
```

## 📖 API Документация

После запуска приложения документация Swagger доступна по адресу:
- `http://localhost:8080/swagger`

### Основные эндпоинты

#### Создание новой игры
```http
POST /api/game?size=3&winLength=3
```

#### Совершение хода
```http
POST /api/move/{gameId}
Content-Type: application/json

{
  "row": 0,
  "column": 1,
  "player": "X"
}
```

#### Получение состояния игры
```http
GET /api/move/game/{gameId}
```

#### Проверка здоровья сервиса
```http
GET /api/health
```

## 🏗 Архитектура проекта

```
TicTacToe.Api/
├── Controllers/          # API контроллеры
├── Models/              # Модели данных
├── Services/            # Бизнес-логика
├── Data/               # Контекст базы данных
├── Migrations/         # Миграции EF Core
├── Test/              # Unit тесты
├── Dockerfile         # Конфигурация Docker
└── docker-compose.yml # Оркестрация контейнеров
```

### Ключевые компоненты

- **GameController**: Управление созданием игр
- **MoveController**: Обработка ходов и получение состояния игры
- **GameService**: Бизнес-логика создания игр
- **MoveService**: Логика обработки ходов
- **GameLogic**: Алгоритмы проверки победы и валидации
- **ErrorHandlingMiddleware**: Глобальная обработка ошибок

## 🧪 Тестирование

```bash
# Запуск всех тестов
dotnet test

# Запуск тестов с покрытием
dotnet test --collect:"XPlat Code Coverage"
```

## 🔧 Конфигурация

### Переменные окружения

- `ConnectionStrings__Postgres`: Строка подключения к PostgreSQL
- `BoardSize`: Размер игрового поля по умолчанию

### Настройки приложения

Конфигурация находится в файлах:
- `appsettings.json`: Основные настройки
- `appsettings.Development.json`: Настройки для разработки

## 🐳 Docker

### Сборка образа
```bash
docker build -t tictactoe-api .
```

### Запуск контейнера
```bash
docker run -p 8080:8080 tictactoe-api
