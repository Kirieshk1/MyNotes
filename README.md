MyNotes
📌 Описание
MyNotes — это веб-приложение для создания и управления заметками с возможностью их распределения по категориям.

🛠 Требования к среде разработки
Установка необходимых компонентов
1️⃣ Установите .NET SDK
Скачайте и установите .NET SDK 8 с официального сайта: Download .NET

2️⃣ Установите PostgreSQL
Скачайте и установите PostgreSQL с официального сайта: Download PostgreSQL

3️⃣ Установите Entity Framework Core CLI
Откройте терминал и выполните команду:
///
dotnet tool install --global dotnet-ef
///
🔧 Настройка подключения к базе данных PostgreSQL
1️⃣ Создайте базу данных PostgreSQL
Откройте терминал и выполните команду:
///
sudo -u postgres psql
///
Введите команду для создания базы данных:
///
CREATE DATABASE MyNotesDB;
///
2️⃣ Создайте пользователя и настройте права
Введите команду для создания пользователя:
///
CREATE USER mynotes_user WITH PASSWORD 'your_password';
///
Настройте права доступа:
///
GRANT ALL PRIVILEGES ON DATABASE MyNotesDB TO mynotes_user
///
3️⃣ Настройте файл appsettings.json
Откройте файл appsettings.json и добавьте строку подключения:
///
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=MyNotesDB;Username=mynotes_user;Password=your_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "EncryptionKey": "YourBase16EncodedEncryptionKeyHere"
}
///
🚀 Инструкции по запуску и тестированию приложения
1️⃣ Клонирование репозитория
sh
Копировать
Редактировать
git clone https://github.com/yourusername/MyNotes.git
cd MyNotes
2️⃣ Применение миграций
sh
Копировать
Редактировать
dotnet ef database update
3️⃣ Запуск приложения
///
dotnet run
///
4️⃣ Тестирование основных функций
Откройте браузер и перейдите по адресу http://localhost:5000 (или другому порту, если он был изменён).

Создание категории:
Введите название категории и нажмите "Создать".
Добавление заметки:
Выберите категорию, введите текст заметки и нажмите "Добавить заметку".
Просмотр заметок:
Нажмите на категорию, чтобы увидеть все её заметки.
Редактирование заметки:
Нажмите "Редактировать", внесите изменения и сохраните.
Удаление заметки:
Нажмите кнопку "Удалить" рядом с заметкой.
Удаление категории:
Нажмите "Удалить категорию", чтобы удалить категорию и все её заметки.
Переключение темы:
Используйте ссылки "Светлая тема" и "Тёмная тема" в навигации.
⚙️ Технологии
C#, ASP.NET Core 8 MVC
Entity Framework Core
PostgreSQL
Bootstrap
Razor Pages
