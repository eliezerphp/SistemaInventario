using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    //Se agrego el public y se hizo que sea generico con <T> y hereda de nuestra interfaz generica IRepositorio
    // implementamos la interfaz en el momento en que IRepositorio nos marca error 
    public class Repositorio<T> : IRepositorio<T> where T : class
    {

        private readonly ApplicationDbContext _db; // hacemos referencia al dbcontext
        internal DbSet<T> dbSet; // objeto de tipo DbSet Generico.

        //Constructor creado
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); //seteamos al objeto y lo ponemos como una propiedad del dbSet
        }

        public async Task Agregar(T entidad)
        {
            //throw new NotImplementedException();

            await dbSet.AddAsync(entidad); //equivale a un Insert into table

        }

        public async Task<T> Obtener(int id)
        {
            //throw new NotImplementedException();

            return await dbSet.FindAsync(id); // Select * from (solo por Id)
        }

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if(filtro != null) 
            {
                query = query.Where(filtro); // select * from where....
            }

            if (incluirPropiedades != null)  // para ver si estan mandando una linea de caracteres
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) // recorremos la linea de caracteres
                {
                    query = query.Include(incluirProp); // ejemplo "Marca, Categoria"
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking(); // QUe no trackee el registro en el caso de que lo estemos utilizando y al mismo tiempo lo quiera actualizar.
            }

            return await query.ToListAsync();
        }

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro); // select * from where....
            }

            if (incluirPropiedades != null)  // para ver si estan mandando una linea de caracteres
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) // recorremos la linea de caracteres
                {
                    query = query.Include(incluirProp); // ejemplo "Marca, Categoria"
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking(); // QUe no trackee el registro en el caso de que lo estemos utilizando y al mismo tiempo lo quiera actualizar.
            }

            return await query.FirstOrDefaultAsync(); // A diferencia del FindAsync, este si acepta filtros
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad); // delete
        }

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
