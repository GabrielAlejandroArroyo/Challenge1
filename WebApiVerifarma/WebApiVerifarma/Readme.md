## Inicializar repo GIT

echo "# Challenge1" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/GabrielAlejandroArroyo/Challenge1.git
git push -u origin main


## Instalar librerias entity framework y automapper


Desde terminal ir a la carpeta de proyecto

dotnet add package Automapper

dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design



dotnet add package Microsoft.CodeAnalysis
dotnet add package Microsoft.CodeAnalysis.Roslyn
dotnet add package Microsoft.CodeAnalysis.CSharp


dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Settings.Configuration
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Sinks.File


dotnet add package Microsoft.ApplicationInsights.AspNetCore


## Comandos Entity Framework

Crea archivo de migracion en carpeta de Migrations

dotnet ef migrations add InicialDb

## Hace la migracion en base a la carpeta Migrations

dotnet ef database update



