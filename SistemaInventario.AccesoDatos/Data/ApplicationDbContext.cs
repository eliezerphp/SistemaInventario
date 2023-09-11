using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.Modelos;
using System.Reflection;

namespace SistemaInventario.AccesoDatos.Data //Se agregó .AccesoDatos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; } //DbSet del modelo bodegas, para que se pueda crear como tabla en la base de datos.
       
        // DbSet del modelo Categorias
        public DbSet<Categoria> Categorias { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) //Agregado para tener mejor control de las actualizaciones de las migraciones
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // hara un override de lo que actualmente existe y tomara esa configuracion  
        }
    }
}