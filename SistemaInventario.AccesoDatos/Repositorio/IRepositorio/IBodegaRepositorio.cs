using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IBodegaRepositorio : IRepositorio<Bodega>//Heredamos de IRepositorio generico y mandamos el modelo Bodega
    {
        void Actualizar(Bodega bodega); // SIempre el actualizar se manejara de manera individual por cada interfaz
    }
}
