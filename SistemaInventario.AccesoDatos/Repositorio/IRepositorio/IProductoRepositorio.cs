using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>//Heredamos de IRepositorio generico y mandamos el modelo Bodega
    {
        void Actualizar(Producto producto); // SIempre el actualizar se manejara de manera individual por cada interfaz

        IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj); //Metodo que utilizaremos en ProductoVM
    }
}
