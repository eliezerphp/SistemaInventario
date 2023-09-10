using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo; //Instanciamos la unidad de trabajo que contiene los Repositorios y sus metodos.

        public BodegaController(IUnidadTrabajo unidadTrabajo)
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
            Bodega bodega = new Bodega();

            if (id == null)
            {
                //Crear una nueva Bodega
                bodega.Estado = true;
                return View(bodega);
            }
            //Actualizamos bodega
            bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if (bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //sirve para evitar la falsificacion de solicitudes de un sitio cargado que puede intentar cargar datos de otra pagina en la nuestra 
        public async Task<IActionResult> Upsert(Bodega bodega) //Llamada del metodo Insert
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Bodega creada Exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Bodega actualizada exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Bodega"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr 
            return View(bodega);
        }

        #region API

        [HttpGet] //ya que obtendremos datos. 
        //El IActionResult no solo retorna una list a lista o una lista, si no, tambien objetos con formas  to JSON
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Bodega.ObtenerTodos();//Metodo definido en el archivo Repositorio en carpeta repositorio en capa AccesoDatos y llamada desde el archivo UnidadTrabajo ubicada en la misma carpeta
            return Json(new {data = todos}); //el "data" es el nombre con el que se referenciara y se mandara a llamar con Javascript

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            var bodegaDb = await _unidadTrabajo.Bodega.Obtener(id);

            if (bodegaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Bodega" });
            }
            _unidadTrabajo.Bodega.Remover(bodegaDb);
            await _unidadTrabajo.Guardar();
            return Json( new { success = true, message = "Bodega borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0) 
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Bodega.ObtenerTodos();

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
