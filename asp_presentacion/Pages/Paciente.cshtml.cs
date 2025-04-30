using lib_entidades.Modelos;
using lib_presentaciones.Interfaces;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class PacienteModel : PageModel
    {
        private readonly IPacientePresentacion _iPresentacion;

        public PacienteModel(IPacientePresentacion iPresentacion)
        {
            _iPresentacion = iPresentacion ?? throw new ArgumentNullException(nameof(iPresentacion));
            Filtro = new Paciente();
            Accion = Enumerables.Ventanas.Listas; // Valor predeterminado
        }

        [BindProperty]
        public Enumerables.Ventanas Accion { get; set; }

        [BindProperty]
        public Paciente? Actual { get; set; }

        [BindProperty]
        public Paciente Filtro { get; set; }

        [BindProperty]
        public List<Paciente>? Lista { get; set; }

        [BindProperty]
        public string? SearchType { get; set; }

        [BindProperty]
        public string? SearchValue { get; set; }

        public async Task OnGetAsync()
        {
            // Acción por defecto al cargar la página
            Accion = Enumerables.Ventanas.Listas;
        }

        public async Task<IActionResult> OnPostBuscarAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(SearchValue))
                {
                    ModelState.AddModelError(string.Empty, "Debe ingresar un valor para realizar la búsqueda.");
                    return Page();
                }

                // Crear un nuevo objeto Filtro según el tipo de búsqueda
                Filtro = new Paciente();

                // Asignar el valor de búsqueda al campo correspondiente según el tipo seleccionado
                switch (SearchType)
                {
                    case "Cedula":
                        Filtro.Cedula = SearchValue;
                        break;
                    case "Nombre":
                        Filtro.Nombre = SearchValue;
                        break;
                    case "Email":
                        Filtro.Email = SearchValue;
                        break;
                    case "Telefono":
                        Filtro.Telefono = SearchValue;
                        break;
                    default:
                        Filtro.Cedula = SearchValue; // Por defecto, buscamos por cédula
                        break;
                }

                Accion = Enumerables.Ventanas.Listas;
                Lista = await _iPresentacion.Buscar(Filtro, "COMPLEJA");

                // Filtrar los resultados según el tipo de búsqueda seleccionado
                if (Lista != null && Lista.Count > 0)
                {
                    List<Paciente> resultadosFiltrados = new List<Paciente>();

                    switch (SearchType)
                    {
                        case "Cedula":
                            resultadosFiltrados = Lista.Where(p => !string.IsNullOrEmpty(p.Cedula) &&
                                p.Cedula.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                            Actual = resultadosFiltrados.FirstOrDefault();
                            break;
                        case "Nombre":
                            resultadosFiltrados = Lista.Where(p => !string.IsNullOrEmpty(p.Nombre) &&
                                p.Nombre.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                            Actual = resultadosFiltrados.FirstOrDefault();
                            break;
                        case "Email":
                            resultadosFiltrados = Lista.Where(p => !string.IsNullOrEmpty(p.Email) &&
                                p.Email.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                            Actual = resultadosFiltrados.FirstOrDefault();
                            break;
                        case "Telefono":
                            resultadosFiltrados = Lista.Where(p => !string.IsNullOrEmpty(p.Telefono) &&
                                p.Telefono.Contains(SearchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                            Actual = resultadosFiltrados.FirstOrDefault();
                            break;
                        default:
                            resultadosFiltrados = Lista;
                            break;
                    }

                    Lista = resultadosFiltrados;
                }

                // Si no se encontró ningún paciente, mostramos un mensaje
                if (Lista == null || Lista.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, $"No se encontró ningún paciente con el {SearchType?.ToLower() ?? "criterio"} ingresado.");
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al buscar el paciente: " + ex.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBtRefrescarAsync()
        {
            try
            {
                Filtro ??= new Paciente();
                Accion = Enumerables.Ventanas.Listas;
                Lista = await _iPresentacion.Buscar(Filtro, "COMPLEJA");
                Actual = null;

                // Si no se encontraron pacientes, mostramos un mensaje
                if (Lista == null || Lista.Count == 0)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron pacientes en el sistema.");
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al refrescar la lista de pacientes: " + ex.Message);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostBtCancelarAsync()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                return await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cancelar la operación: " + ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostBtCerrarAsync()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                {
                    return await OnPostBtRefrescarAsync();
                }

                return Page();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cerrar: " + ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEditarAsync(string pacienteId)
        {
            try
            {
                Filtro = new Paciente { Cedula = pacienteId };
                var resultados = await _iPresentacion.Buscar(Filtro, "COMPLEJA");
                Actual = resultados?.FirstOrDefault();

                if (Actual == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontró el paciente para editar.");
                    return await OnPostBtRefrescarAsync();
                }

                // Aquí implementarías la lógica para editar
                // Por ahora solo seleccionamos el paciente
                Accion = Enumerables.Ventanas.Listas;
                return Page();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al editar el paciente: " + ex.Message);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(string pacienteId)
        {
            try
            {
                // Aquí implementarías la lógica para eliminar
                // Por ahora solo mostramos un mensaje
                ModelState.AddModelError(string.Empty, "Función de eliminación aún no implementada.");
                return await OnPostBtRefrescarAsync();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el paciente: " + ex.Message);
                return Page();
            }
        }
    }
}