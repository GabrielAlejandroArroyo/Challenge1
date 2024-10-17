using WebApiVeriframa.DTOs;


namespace WebApiVeriframa.Services.Interfaces
{
    public interface IFarmaciaService
    {
        Task<IEnumerable<FarmaciaReadDTO>> GetAllAsync();
        Task<FarmaciaReadDTO> GetByIdAsync(int id);
        Task<FarmaciaReadDTO> CreateAsync(FarmaciaCreateDTO objectDTO);
        Task<bool> UpdateAsync(int id, FarmaciaUpdateDTO objectDTO);
        Task<bool> DeleteAsync(int id);
        Task<FarmaciaReadDTO> GetFarmaciaMasCercana(decimal latitud, decimal longitud);
    }
}
