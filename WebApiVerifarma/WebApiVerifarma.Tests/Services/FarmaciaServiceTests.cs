using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiVerifarma.DTOs.Farmacia;
using WebApiVeriframa.Data;
using WebApiVeriframa.Models;
using WebApiVeriframa.Services.Implementation;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace WebApiVerifarma.Tests.Services
{
    public class FarmaciaServiceTests
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;
        private readonly AppDbContext _context;
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;
        private readonly FarmaciaService _service;

        public FarmaciaServiceTests()
        {
            // Configuración de la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(_dbContextOptions);

            // Configuración del mapeo utilizando AutoMapper
            _config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Farmacia, FarmaciaReadDTO>();
                cfg.CreateMap<FarmaciaReadDTO, Farmacia>();
                cfg.CreateMap<FarmaciaCreateDTO, Farmacia>();
            });

            _mapper = _config.CreateMapper();

            _service = new FarmaciaService(_context, _mapper);
        }


        [Fact]
        public async Task CreateAsync_AddsNewFarmaciaSucces_WhenCalled()
        {

            // Arrange
            var newFarmacia = new FarmaciaCreateDTO { Nombre = "Farmacia Nueva", Latitud = 20.0m, Longitud = 20.0m, Direccion = "123 Calle Falsa" };
            var mappedFarmacia = new Farmacia { Nombre = "Farmacia Nueva", Latitud = 20.0m, Longitud = 20.0m, Direccion = "123 Calle Falsa" };
            var service = new FarmaciaService(_context, _mapper);

            // Act
            var result = await service.CreateAsync(newFarmacia);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mappedFarmacia.Latitud, result.Latitud);
            Assert.Equal(mappedFarmacia.Longitud, result.Longitud);
            Assert.Equal(mappedFarmacia.Nombre, result.Nombre);
            Assert.Equal(mappedFarmacia.Direccion, result.Direccion);
        }

        [Fact]
        public async Task CreateAsync_AddsNewFarmaciaNotSucces_WhenCalled()
        {
            // Arrange
            // Creando un DTO sin valores requeridos para provocar un fallo en la creación
            var newFarmaciaInvalida = new FarmaciaCreateDTO { Nombre = "", Latitud = 0.0m, Longitud = 0.0m, Direccion = "" };
            var service = new FarmaciaService(_context, _mapper);

            // Act
            async Task action() => await service.CreateAsync(newFarmaciaInvalida);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(action);
        }

        [Fact]
        public async Task GetFarmaciaMasCercana_ReturnsClosestFarmacia_WhenCalled()
        {
            // Arrange
            _context.Farmacias.AddRange(new List<Farmacia>
            {
                new Farmacia { Nombre = "Farmacia A", Direccion = "Calle A", Latitud = 10.0m, Longitud = 10.0m },
                new Farmacia { Nombre = "Farmacia B", Direccion = "Calle B", Latitud = 15.0m, Longitud = 15.0m }
            });
            await _context.SaveChangesAsync();
            var service = new FarmaciaService(_context, _mapper);

            // Act
            var result = await service.GetFarmaciaMasCercana(12.0m, 12.0m);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(10.0m, result.Latitud);
            Assert.Equal(10.0m, result.Longitud);
            Assert.Equal("Farmacia A", result.Nombre);
            Assert.Equal("Calle A", result.Direccion);
        }

    }
}
