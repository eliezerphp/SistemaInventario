using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo; //Instanciamos la unidad de trabajo que contiene los Repositorios y sus metodos.

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
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
            Categoria categoria = new Categoria();

            if (id == null)
            {
                //Crear una nueva Bodega
                categoria.Estado = true;
                return View(categoria);
            }
            //Actualizamos bodega
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //sirve para evitar la falsificacion de solicitudes de un sitio cargado que puede intentar cargar datos de otra pagina en la nuestra 
        public async Task<IActionResult> Upsert(Categoria categoria) //Llamada del metodo Insert
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria creada Exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada exitosamente"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar Categoria"; //Temp data y mensaje que recibe TempData en el partialView _Notificaciones, sera el mensaje que mostrara la notificacion Toastr 
            return View(categoria);
        }

        #region API

        [HttpGet] //ya que obtendremos datos. 
        //El IActionResult no solo retorna una list a lista o una lista, si no, tambien objetos con formas  to JSON
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();//Metodo definido en el archivo Repositorio en carpeta repositorio en capa AccesoDatos y llamada desde el archivo UnidadTrabajo ubicada en la misma carpeta
            return Json(new {data = todos}); //el "data" es el nombre con el que se referenciara y se mandara a llamar con Javascript

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) 
        {
            var categoriaDb = await _unidadTrabajo.Categoria.Obtener(id);

            if (categoriaDb == null)
            {
                return Json(new { success = false, message = "Error al borrar Categoria" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaDb);
            await _unidadTrabajo.Guardar();
            return Json( new { success = true, message = "Categoria borrada exitosamente" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0) 
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();

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
