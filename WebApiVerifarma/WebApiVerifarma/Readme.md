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




## Documentacion de API en Swagger

https://localhost:7196/swagger/index.html


## Estructura de proyecto

WebApiVerifarma/
│
├── Controllers/
│ ├── FarmaciaController.cs

│
├── DTOs/
│ ├── Farmacia
│ ├──── FarmaciaCreateDTO
│ ├──── FarmaciaReadDTO.cs
│ ├──── FarmaciaDeleteDTO.cs
│ ├──── FarmaciaUpdateDTO.cs
│ ├── Otros DTOS
│
├── Models/
│ ├── Farmacia.cs
│
├── Services/
├──-── Implementation/
├──-── Implementation/FarmaciaService.cs
├──-── Interfaces/
├──-── Interfaces/IFarmaciaService.cs
│
├── Mappings/
│ ├── MappingProfile.cs
│
├── Data/
│ ├── ApplicationDbContext.cs
│
├── Startup.cs (para ASP.NET Core 5 y versiones anteriores)
│
├── Program.cs (para ASP.NET Core 6 y versiones posteriores)
│
├── appsettings.json
│
├── appsettings.Development.json
│
└── ... otros archivos y carpetas necesarios


## Descripción de la Estructura Proyecto

### Controllers

Contiene los controladores de la API, que gestionan las solicitudes HTTP y devuelven las respuestas.
- ` FarmaciaController.cs`: Controlador para gestionar Farmacias.

### DTOs

Contiene los Data Transfer Objects (DTOs), que son objetos simples usados para transferir datos entre el cliente y el servidor.
- `FarmaciaCreateDTO.cs`: DTOs para el modelo `CocomoState`.
- `EmployeeDTOs.cs`: DTOs para el modelo `Employee` (ejemplo previo).

### Models

Contiene las clases de modelo que representan la estructura de los datos.
- `CocomoState.cs`: Modelo para `CocomoState`.
- `Employee.cs`: Modelo para `Employee` (ejemplo previo).

### Services

Contiene las interfaces y las implementaciones de los servicios, que encapsulan la lógica de negocio y las operaciones CRUD.
- `ICocomoStateService.cs`: Interfaz para el servicio `CocomoState`.
- `CocomoStateService.cs`: Implementación del servicio `CocomoState`.
- `IEmployeeService.cs`: Interfaz para el servicio `Employee` (ejemplo previo).
- `EmployeeService.cs`: Implementación del servicio `Employee` (ejemplo previo).

### Mappings

Contiene las configuraciones de AutoMapper.
- `MappingProfile.cs`: Configuración de AutoMapper para mapear entre los modelos y los DTOs.

### Data

Contiene la clase del contexto de la base de datos.
- `ApplicationDbContext.cs`: Contexto de Entity Framework Core que maneja la conexión a la base de datos y las entidades.

### Startup.cs

Archivo de configuración para ASP.NET Core 5 y versiones anteriores, donde se registran los servicios y se configuran los middleware.

### Program.cs

Archivo de configuración para ASP.NET Core 6 y versiones posteriores, donde se configuran los servicios y se define la tubería de solicitud.

### appsettings.json

Archivo de configuración para las opciones de la aplicación.

### appsettings.Development.json

Archivo de configuración específico para el entorno de desarrollo.

### appsettings.Production.json

Archivo de configuración específico para el entorno de producción.

### Prospection.Seed.csproj

Archivo de proyecto de .NET que define las dependencias y configuraciones del proyecto.

Esta estructura sigue las prácticas recomendadas para la organización de un proyecto ASP.NET Core, facilitando la separación de preocupaciones y el mantenimiento del código.





