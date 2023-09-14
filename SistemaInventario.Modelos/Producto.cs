using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Descripcion es requerido")]
        [MaxLength(60)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Numero de serie es requerido")]
        [MaxLength(60)]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "Precio es requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Costo es requerido")]
        public double Costo { get; set; }

        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "Categoria es requerido")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")] // Definir llave foranea
        public Categoria Categoria { get; set; } //Navegacion, es decir, podemos acceder a los campos de la propiedad

        [Required(ErrorMessage = "Marca es requerido")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")] // Definir llave foranea
        public Marca Marca { get; set; } //Navegacion, es decir, podemos acceder a los campos de la propiedad


        //Esto es recursividad, un producto puede estar relacionado al mismo producto
        public int? PadreId { get; set; } // Se pone el signo ya que si no lo ponemos el int se guardara como "0" y asi nos dara problemas por se llave primaria, con el signo se guardara como null
        public virtual Producto Padre { get; set; }
        

    }

}
