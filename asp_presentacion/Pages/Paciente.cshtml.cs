using lib_entidades.Modelos;
using lib_presentaciones.Interfaces;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class PacienteModel : PageModel
    {

        public PacienteModel(IPacientePresentacion iPresentacion)
        {
            Filtro = new Paciente();
            Accion = Enumerables.Ventanas.Listas; // Valor predeterminado
        }


        {
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


                }
            }
                catch (Exception ex)
                {
                    LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al buscar el paciente: " + ex.Message);
                }

            return Page();
            }
        }

        {
            try
            {
                Filtro ??= new Paciente();

                Accion = Enumerables.Ventanas.Listas;
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

        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                ModelState.AddModelError(string.Empty, "Ocurrió un error al cancelar la operación: " + ex.Message);
                return Page();
            }
        }

        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
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