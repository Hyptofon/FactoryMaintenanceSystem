# Factory Maintenance System

## Опис

Factory Maintenance System — це RESTful Web API, розроблене для автоматизації та управління процесами технічного обслуговування на виробництві. Система дозволяє керувати обліком обладнання, планувати регулярне технічне обслуговування та відстежувати виконання ремонтних робіт (ворк-ордерів).

Проект побудований на сучасній платформі .NET 9 з використанням принципів Clean Architecture та патерну CQRS (Command Query Responsibility Segregation).

## Основна функціональність

Система надає API для виконання наступних завдань:

  * **Управління обладнанням (Equipment):**
      * Створення, оновлення та перегляд карток обладнання.
      * Відстеження статусу обладнання.
  * **Планування обслуговування (Maintenance Schedules):**
      * Створення розкладів планового технічного обслуговування.
      * Оновлення та деактивація розкладів.
  * **Наряди на роботу (Work Orders):**
      * Створення нових нарядів на ремонт або обслуговування.
      * Життєвий цикл наряду: Створено -\> В роботі -\> Виконано (або Скасовано).
      * Призначення відповідальних та опис виконаних робіт.

## Технологічний стек

  * **Платформа:** .NET 9
  * **Архітектура:** Clean Architecture (Api, Application, Domain, Infrastructure)
  * **База даних:** PostgreSQL
  * **ORM:** Entity Framework Core
  * **Підхід до взаємодії:** CQRS (за допомогою бібліотеки **MediatR**)
  * **Валідація:** FluentValidation
  * **Документація API:** Swagger / OpenAPI
  * **Логування:** Стандартний `Microsoft.Extensions.Logging`

## Структура проекту

Рішення розділене на наступні шари:

  * **src/Domain** — Містить сутності бізнес-логіки (`Equipment`, `MaintenanceSchedule`, `WorkOrder`) та основні правила. Не має зовнішніх залежностей.
  * **src/Application** — Містить бізнес-логіку застосунку, реалізовану через команди та запити (CQRS), інтерфейси, DTO та валідатори.
  * **src/Infrastructure** — Реалізація взаємодії з зовнішніми системами, зокрема з базою даних (EF Core DbContext, репозиторії, міграції PostgreSQL).
  * **src/Api** — Точка входу в застосунок. Містить контролери (`EquipmentController`, `WorkOrdersController` тощо), конфігурацію DI та Middleware.

## Початок роботи

### Вимоги

  * .NET 9 SDK
  * PostgreSQL Server

### Налаштування та запуск

1.  **Клонуйте репозиторій:**

    ```bash
    https://github.com/Hyptofon/FactoryMaintenanceSystem.git
    ```

2.  **Налаштуйте базу даних:**
    Відкрийте файл `src/Api/appsettings.json` (або `appsettings.Development.json`) та вкажіть коректний рядок підключення до вашого PostgreSQL сервера:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=FactoryMaintenanceDb;Username=postgres;Password=yourpassword"
    }
    ```

3.  **Застосуйте міграції:**
    Перейдіть у папку з проектом API та виконайте команду для створення бази даних:

    ```bash
    cd src/Api
    dotnet ef database update
    ```

4.  **Запустіть проект:**

    ```bash
    dotnet run
    ```

    Після запуску API документація Swagger зазвичай доступна за адресою `https://localhost:7xxx/swagger` (порт залежить від ваших налаштувань у `launchSettings.json`).
