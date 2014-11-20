project-one
===========

Ini adalah proyek pertama animous consulting

### Teknologi
ADO.NET Entity Framework 5, ASP.NET, ASP.NET MVC 4, Entity Framework

### Topik
ASP.NET Code Sample Downloads

### Platform
Web

### Persyaratan

### Bahasa Utama
id

### Diperbarui
20/11/2014

### Lisensi
[Apache License, Version 2.0](file:///Users/astondihor/Downloads/ASP1/license.rtf)

## Pengantar
Sebuah proyek Visual Studio 2013 yang menunjukkan bagaimana menggunakan Entity Framework 5 dalam proyek aplikasi web ASP.NET MVC 4.

### Penggunaan Repository Ini

Repository ini dimaksudkan sebagai sebuah pengerjaan proyek bersama yang tadinya untuk kita bertiga. Tetapi karena abang belum punya akun github, jadi hanya kita berdua saja untuk sementara.

Jadi kita mengerjakan secara team.

Untuk yang ini, karena saya ambil atau pilih yang gratis, maka saya pilih yang open-source, dimana public dapat melihat isi dari repository ini. Kalau mau private ya harus pilih yang berbayar.

## Memulai
Untuk membangun dan menjalankan sampel ini, Anda harus menginstall Visual Studio 2013 atau Visual Studio 2013 Express for Web terlebih dahulu.

Dalam kebanyakan kasus, Anda dapat menjalankan aplikasi dengan mengikuti langkah-langkah berikut:

- Download and extract the .zip file.
- Open the solution file in Visual Studio.
- Build the solution, which automatically installs the missing NuGet packages.
- Open the Package Manager Console, and run the update-database command to create the database.
- Run the application.

If you have any problems with those instructions, follow these longer instructions.
- Download the .zip file.
- In File Explorer, right-click the .zip file and click Properties, then in the Properties window click Unblock.
- Unzip the file.
- Double-click the .sln file to launch Visual Studio.
- From the Tools menu, click Library Package Manager, then Package Manager Console.
- In the Package Manager Console (PMC), click Restore.
- Exit Visual Studio.
- Restart Visual Studio, opening the solution file you closed in the previous step.
- In the Package Manager Console (PMC), enter the Update-Database command. (If you get the following error:
- "The term 'Update-Database' is not recognized as the name of a cmdlet, function, script file, or operable program", exit and restart Visual Studio.)
- Each migration will run, then the seed method will run. You can now run the application.

### Source Code Overview
The aspmvc4 folder includes the following folders and files
App_Data folder - Holds the SQL Server Compact database file.
Content - Holds CSS files.
Controllers - Holds controller classes.
DAL folder - The data access layer.  Holds the context, initializer, repository, and unit of work classes.
Logging folder - Holds code that does logging.
Migrations folder - Holds EF Code First migrations code, including the Seed method.
Models folder - Holds model classes.
Properties or MyProject folder - Project properties.
Scripts folder - Script files.
ViewModels folder - Holds view model classes. 
Views folder - Holds view classes.
Visual Studio project file (.csproj or .vbproj).
packages.config - Specifies NuGet packages included in the project.
Global.asax file - Includes database initializer code.
Web.config file - Includes the connection string to the database.
