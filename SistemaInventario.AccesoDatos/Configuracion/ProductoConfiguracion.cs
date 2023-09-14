using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Configuracion
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto> //agregado public
    {
        public void Configure(EntityTypeBuilder<Producto> builder) // Agregado
        {
            // campos de la tabla
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NumeroSerie).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Costo).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.MarcaId).IsRequired();

            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.PadreId).IsRequired(false);

            /* Relaciones */

            builder.HasOne(x => x.Categoria).WithMany() //Relacion uno a muchos hace referencia a la navegacion
                   .HasForeignKey(x => x.CategoriaId) // definir el campo
                   .OnDelete(DeleteBehavior.NoAction); // que no haga cambios si se elimina un registro maestro 

            builder.HasOne(x => x.Marca).WithMany()
                   .HasForeignKey(x => x.MarcaId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Padre).WithMany()
                   .HasForeignKey(x => x.PadreId)
                   .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
