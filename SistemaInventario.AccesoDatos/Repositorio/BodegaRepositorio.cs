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
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio 
    {

        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db) // Se le pasa el DBcontext al Padre
        {
            _db = db;
        }
        public void Actualizar(Bodega bodega)
        {
            var bodegaBD = _db.Bodegas.FirstOrDefault(b => b.Id == bodega.Id); //Primero capturamos el registro con el Id

            if (bodegaBD != null) //Si se encuentran datos
            {
                bodegaBD.Nombre = bodega.Nombre; //
                bodegaBD.Descripcion = bodega.Descripcion;
                bodegaBD.Estado = bodega.Estado;
                _db.SaveChanges(); //Para ejecutar el update
            }
        }
    }
}
