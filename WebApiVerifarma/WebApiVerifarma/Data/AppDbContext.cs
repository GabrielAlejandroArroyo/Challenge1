using Microsoft.EntityFrameworkCore;
using WebApiVeriframa.Models;

namespace WebApiVeriframa.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Farmacia> Farmacias { get; set; }

    }




}
