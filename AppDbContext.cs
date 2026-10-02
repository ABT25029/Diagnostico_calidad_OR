using Microsoft.EntityFrameworkCore;

namespace API;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Esta propiedad representa nuestra tabla de históricos en la base de datos
    public DbSet<RegistroAgua> HistorialPlanta { get; set; }
}
