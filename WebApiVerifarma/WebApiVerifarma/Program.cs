using Microsoft.EntityFrameworkCore;
using WebApiVeriframa.Data;
using WebApiVeriframa.Mappings;
using WebApiVeriframa.Services.Interfaces;
using WebApiVeriframa.Services.Implementation;
using log4net;
using log4net.Config;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


// Configurar autenticación JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("x-pagination"); // Expone el encabezado x-pagination sera usado para enviar busquedas por post y ser paginadas desde el front end
    });
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Permite cualquier URL/origen
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("x-pagination"); // Expone el encabezado x-pagination sera usado para enviar busquedas por post y ser paginadas desde el front end
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DbContext and configure services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Mappings - Instancia para Automaping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Mapeo por Servicios
builder.Services.AddScoped<IFarmaciaService, FarmaciaService>();

var app = builder.Build();

// Inicializa log4net
var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiVerifarma.api");
        c.RoutePrefix = string.Empty;
    });

}

//app.UseCors();
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
