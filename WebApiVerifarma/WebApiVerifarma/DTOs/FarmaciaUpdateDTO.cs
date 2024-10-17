namespace WebApiVeriframa.DTOs
{
    public class FarmaciaUpdateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}
