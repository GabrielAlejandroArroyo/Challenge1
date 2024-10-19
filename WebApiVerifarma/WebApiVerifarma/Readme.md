## Inicializar repo GIT

echo "# Challenge1" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/GabrielAlejandroArroyo/Challenge1.git
git push -u origin main


## Instalar librerias entity framework y automapper


Desde terminal ir a la carpeta de proyecto WebApiVerifarma

dotnet add package Automapper
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package log4net

dotnet add WebApiVerifarma.Tests package Moq

Desde terminal ir a la carpeta de proyecto WebApiVerifarma.Test


#Levata y genera tablas en la base de datos

## Comandos Entity Framework

Crea archivo de migracion en carpeta de Migrations

dotnet ef migrations add InicialDb

## Hace la migracion en base a la carpeta Migrations

dotnet ef database update




## La definicion de la API se expone en Swagger 

Url, para pruebas 
https://localhost:7196/swagger/index.html
