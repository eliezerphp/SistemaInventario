using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio 
    {

        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db) // Se le pasa el DBcontext al Padre
        {
            _db = db;
        }
        public void Actualizar(Producto producto)
        {
            var productoBD = _db.Productos.FirstOrDefault(b => b.Id == producto.Id); //Primero capturamos el registro con el Id

            if (productoBD != null) //Si se encuentran datos
            {
                if (producto.ImagenUrl != null) //Se actualizara solamente si cambian la imagen, si no, mantendra la que ya tiene
                {
                    productoBD.ImagenUrl = producto.ImagenUrl;
                }

                productoBD.NumeroSerie = producto.NumeroSerie;
                productoBD.Descripcion = producto.Descripcion;
                productoBD.Precio = producto.Precio;
                productoBD.Costo = producto.Costo;
                productoBD.CategoriaId = producto.CategoriaId;
                productoBD.MarcaId = producto.MarcaId;
                productoBD.PadreId = producto.PadreId;
                productoBD.Estado = producto.Estado;

                _db.SaveChanges(); //Para ejecutar el update
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj) //Interfaz implementada
        {
            if (obj == "Categoria")
            {
                return _db.Categorias.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }
                // OBJETO DE TIPO STRING QUE ALMANCENARA LOS DATOS NOMBRE Y ID DE LAS TABLAS CATEGORIA O MARCAS Y A SU VEZ PODER ALIMENTAR EL SELECT EN HTML 
            if (obj == "Marca")
            {
                return _db.Marcas.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
            }

            if (obj == "Producto") //Recursividad para que me muestre los productos en una lista
            {
                return _db.Productos.Where(c => c.Estado == true).Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }

            return null;
        }
    }
}
