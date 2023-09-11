using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo // Hereda de IUnidadTrabajo, luego se implementa la Interfaz
    { 
        //La unidad de trabajo envolvera a cada uno de los repositorios de los Modelos
        //podremos usar la unidad de trabajo en cualquier momento y tener acceso a todos los repositorios
        //para que este accesible en todo el proyecto, debemos agregar la UnidadTrabajo como un servicio en el archivo program.cs

            private readonly ApplicationDbContext _db;
            public IBodegaRepositorio Bodega {  get; private set; }
            public ICategoriaRepositorio Categoria { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
            {
                _db = db;
                Bodega = new BodegaRepositorio(_db);
                Categoria = new CategoriaRepositorio(_db);

        }

            public void Dispose()
            {
                _db.Dispose();
            }

            public async Task Guardar()
            {
                await _db.SaveChangesAsync(); 
            }
    }
}
