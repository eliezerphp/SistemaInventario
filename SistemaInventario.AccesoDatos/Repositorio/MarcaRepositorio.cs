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
    public class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio 
    {

        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db) // Se le pasa el DBcontext al Padre
        {
            _db = db;
        }
        public void Actualizar(Marca marca)
        {
            var marcaBD = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id); //Primero capturamos el registro con el Id

            if (marcaBD != null) //Si se encuentran datos
            {
                marcaBD.Nombre = marca.Nombre; //
                marcaBD.Descripcion = marca.Descripcion;
                marcaBD.Estado = marca.Estado;
                _db.SaveChanges(); //Para ejecutar el update
            }
        }
    }
}
