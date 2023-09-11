using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MarcaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo; //Instanciamos la unidad de trabajo que contiene los Repositorios y sus metodos.

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        //El upsert es una combinacion del Insert y el Update en una sola funcion
        public async Task<IActionResult> Upsert(int? id)// el signo de interrogacion es por que podria no recibir algun dato el id
        {
            Marca marca = new Marca();

            if (id == null)
            {
                //Crear una nueva Bodega
                marca.Estado = true;
                return View(marca);
            }
            //Actualizamos bodega
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //sirve para evitar la falsificacion de solicitudes de un sitio cargado que puede intentar cargar datos de otra pagina en la nuestra 
        public async Task<IActionResult> Upsert(Marca marca) //Llamada del metodo Insert
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada Exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Marca"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr 
            return View(marca);
        }

        #region API

        [HttpGet] //ya que obtendremos datos. 
        //El IActionResult no solo retorna una list a lista o una lista, si no, tambien objetos con formas  to JSON
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.ObtenerTodos();//Metodo definido en el archivo Repositorio en carpeta repositorio en capa AccesoDatos y llamada desde el archivo UnidadTrabajo ubicada en la misma carpeta
            return Json(new {data = todos}); //el "data" es el nombre con el que se referenciara y se mandara a llamar con Javascript

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            var marcaDb = await _unidadTrabajo.Marca.Obtener(id);

            if (marcaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Marca" });
            }
            _unidadTrabajo.Marca.Remover(marcaDb);
            await _unidadTrabajo.Guardar();
            return Json( new { success = true, message = "Marca borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0) 
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { data = true});
            }
            return Json(new {data = false });
        }
        #endregion
    }
}
