using CIAbmc.Models;
using Microsoft.EntityFrameworkCore;

namespace CIAbmc.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        { 
        
        }

        public DbSet<Szablon> Szablony { get; set; }
    }
}
