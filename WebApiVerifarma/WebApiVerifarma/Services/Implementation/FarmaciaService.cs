using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using WebApiVerifarma.DTOs.Farmacia;
using WebApiVeriframa.Data;
using WebApiVeriframa.Models;
using WebApiVeriframa.Services.Interfaces;

namespace WebApiVeriframa.Services.Implementation
{
    public class FarmaciaService : IFarmaciaService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(FarmaciaService));
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly List<Farmacia> _farmacias;

        public FarmaciaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FarmaciaReadDTO> CreateAsync(FarmaciaCreateDTO objectDTO)
        {

            if (string.IsNullOrWhiteSpace(objectDTO.Nombre) || string.IsNullOrWhiteSpace(objectDTO.Direccion))
            {
                throw new ArgumentException("Nombre y Dirección son campos obligatorios.");
            }

            var resultado = _mapper.Map<Farmacia>(objectDTO);

            _context.Farmacias.Add(resultado);
            await _context.SaveChangesAsync();

            return _mapper.Map<FarmaciaReadDTO>(resultado);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FarmaciaReadDTO>> GetAllAsync()
        {
            var resultados = await _context.Farmacias.ToListAsync();
            return _mapper.Map<IEnumerable<FarmaciaReadDTO>>(resultados);
        }

        public async Task<FarmaciaReadDTO> GetByIdAsync(int id)
        {
            var resultados = await _context.Farmacias.FindAsync(id);

            if (resultados == null)
            {
                return null;
            }
            return _mapper.Map<FarmaciaReadDTO>(resultados);
        }

        public async Task<bool> UpdateAsync(int id, FarmaciaUpdateDTO objectDTO)
        {
            if (id != objectDTO.Id)
            {
                return false;
            }

            var resultado = await _context.Farmacias.FindAsync(id);

            if (resultado == null)
            {
                return false;
            }

            _mapper.Map(objectDTO, resultado);

            _context.Entry(resultado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<FarmaciaReadDTO> GetFarmaciaMasCercana(decimal latitud, decimal longitud)
        {
            var distanciaMaxima = decimal.MaxValue;
            var farmaciaMasCercana = new FarmaciaReadDTO();
            var resultados = await this.GetAllAsync();
            
            foreach (var resultado in resultados)
            {
                var distancia = Haversine(latitud, longitud, resultado.Latitud, resultado.Longitud);
                if (distancia < distanciaMaxima)
                {
                    distanciaMaxima = distancia;
                    farmaciaMasCercana = resultado;
                }
            }

            return _mapper.Map<FarmaciaReadDTO>(farmaciaMasCercana); 
        }

        private decimal Haversine(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            var R = 6371; // Radio de la Tierra en kilómetros
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var lat1Rad = ToRadians(lat1);
            var lat2Rad = ToRadians(lat2);

            var a = Math.Sin((double)dLat / 2) * Math.Sin((double)dLat / 2)
                + Math.Sin((double)dLon / 2) * Math.Sin((double)dLon / 2) * Math.Cos((double)lat1Rad) * Math.Cos((double)lat2Rad);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (decimal)R * (decimal)c;
        }

        private decimal ToRadians(decimal angleInDegrees)
        {
            return angleInDegrees * (decimal)Math.PI / 180;
        }
    }
}
