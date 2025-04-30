using lib_entidades.Modelos;
using lib_presentaciones.Interfaces;
using lib_utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace asp_presentacion.Pages
{
    public class PacienteModel : PageModel
    {
        private IPacientePresentacion? iPresentacion;

        public PacienteModel(IPacientePresentacion iPresentacion)
        {
            this.iPresentacion = iPresentacion;
            Filtro = new Paciente();
        }

        [BindProperty] public Enumerables.Ventanas Accion { get; set; }
        [BindProperty] public Paciente? Actual { get; set; }
        [BindProperty] public Paciente? Filtro { get; set; }
        [BindProperty] public List<Paciente>? Lista { get; set; }

        public void OnPostBuscar()
        {
            if (!string.IsNullOrEmpty(Filtro?.Cedula))
            {
                try
                {
                    Accion = Enumerables.Ventanas.Listas;

                    var task = this.iPresentacion!.Buscar(Filtro!, "CEDULA_PACIENTE");
                    task.Wait();
                    Lista = task.Result;

                    // Buscar exactamente por la cédula digitada
                    Actual = Lista?.FirstOrDefault(h => h.Cedula == Filtro.Cedula);
                    // Limpiar el campo después de buscar
                    Filtro.Cedula = "";
                }
                catch (Exception ex)
                {
                    LogConversor.Log(ex, ViewData!);
                }
            }
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                Filtro ??= new Paciente();

                Accion = Enumerables.Ventanas.Listas;
                var task = iPresentacion!.Buscar(Filtro!, "COMPLEJA");
                task.Wait();
                Lista = task.Result;
                Actual = null;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCancelar()
        {
            try
            {
                Accion = Enumerables.Ventanas.Listas;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtCerrar()
        {
            try
            {
                if (Accion == Enumerables.Ventanas.Listas)
                    OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}