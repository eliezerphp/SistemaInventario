using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{

    //el IDisposable te permite deshacerte de cualquier recurso que haya obtenido el sistema y liberar objetos
    //que ya no esten usando y que esten consumiendo recursos innecesariamente
    internal interface IUnidadTrabajo : IDisposable
    {
    }
}