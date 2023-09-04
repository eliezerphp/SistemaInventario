using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    //Agregado Public. 
    //Para que acepte todas las clases se le agrega el parametro T donde T puede ser igual a una clase
    // de esta manera se hace que la interfaz sea generica
    public interface IRepositorio<T> where T : class 
    {
        
        //Para hacer estos metodos en metodos asincronos se utiliza Task<>
        // Solo para obtener ID
        Task<T> Obtener(int id);

        //IEnumerable, almacena y recibe listas
        Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task Agregar(T entidad);

        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidad);
    }
}
