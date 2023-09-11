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
    //se hereda de repositorio, le mandamos el modelo Bodega y tambien hereda de IBodegaRepositorio
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio 
    {

        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db) // Se le pasa el DBcontext al Padre
        {
            _db = db;
        }
        public void Actualizar(Categoria categoria)
        {
            var categoriaBD = _db.Categorias.FirstOrDefault(b => b.Id == categoria.Id); //Primero capturamos el registro con el Id

            if (categoriaBD != null) //Si se encuentran datos
            {
                categoriaBD.Nombre = categoria.Nombre; //
                categoriaBD.Descripcion = categoria.Descripcion;
                categoriaBD.Estado = categoria.Estado;
                _db.SaveChanges(); //Para ejecutar el update
            }
        }
    }
}
