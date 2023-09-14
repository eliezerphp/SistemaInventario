using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.ViewModels
{
    public class ProductoVM
    {
        public Producto Producto { get; set; }

        public IEnumerable<SelectListItem> CategoriaLista { get; set; }// El SelectListItem dara error, hay que instalar el paquete y agregar la referencia arriba 
        public IEnumerable<SelectListItem> MarcaLista { get; set; }
        public IEnumerable<SelectListItem> PadreLista { get; set; }
    }
}
