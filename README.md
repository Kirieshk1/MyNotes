MyNotes – Веб-приложение для заметок

MyNotes – это веб-приложение, позволяющее удобно создавать, редактировать, организовывать и удалять заметки. Приложение поддерживает категории, темную и светлую темы, а также валидацию и шифрование данных.

🛠 Технологии

C#, ASP.NET Core 8 MVC

Entity Framework Core

PostgreSQL

Bootstrap (UI стилизация)

JavaScript & jQuery (интерактивность)

📥 Установка и запуск

1️⃣ Установка зависимостей

Перед началом работы убедитесь, что у вас установлены:

.NET 8 SDK

PostgreSQL

2️⃣ Настройка базы данных

Создайте ключ TinyMCE: Ключ для TinyMCE вы можете бесплатно создать на их официальном сайте.

Откройте файл appsettings.json и укажите параметры подключения к базе данных:

{
   "ConnectionStrings": {
   "DefaultConnection": "Host=localhost;Database=NotesApp;Username=username;Password=your_password"
   },
   "Logging": {
     "LogLevel": {
       "Default": "Information",
       "Microsoft.AspNetCore": "Warning"
     }
   },
   "TinyMCE": {
     "ApiKey": "yourApiKey"
   },
   "AllowedHosts": "*",
   "EncryptionKey": "YourBase16EncodedEncryptionKeyHere"
}

Примените миграции и создайте базу данных:

dotnet ef database update

3️⃣ Запуск приложения

Запустите приложение командой:

   dotnet run

Приложение будет доступно по адресу: http://localhost:5000
