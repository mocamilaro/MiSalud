using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class DiagnosticoAplicacion : IDiagnosticoAplicacion
    {
        private IDiagnosticoRepositorio? iRepositorio = null;

        public DiagnosticoAplicacion(IDiagnosticoRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Diagnostico Borrar(Diagnostico entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdDiagnostico == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Diagnostico Guardar(Diagnostico entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdDiagnostico != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Diagnostico> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Diagnostico> Buscar(Diagnostico entidad, string tipo)
        {
            Expression<Func<Diagnostico, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "DESCRIPCION":
                    condiciones = x => x.Descripcion != null &&
                                     x.Descripcion.Contains(entidad.Descripcion!);
                    break;

                case "FECHA":
                    condiciones = x => x.Fecha == entidad.Fecha;
                    break;

                case "MEDICO":
                    condiciones = x => x.Medico != null &&
                                     (x.Medico.Nombre != null && x.Medico.Nombre.Contains(entidad.Medico!.Nombre!));
                    break;

                case "PACIENTE":
                    condiciones = x => x.HistoriaClinica != null &&
                                      x.HistoriaClinica.Paciente != null &&
                                      (x.HistoriaClinica.Paciente.Nombre != null &&
                                       x.HistoriaClinica.Paciente.Nombre.Contains(entidad.HistoriaClinica!.Paciente!.Nombre!));
                    break;

                case "CEDULA_PACIENTE":
                    condiciones = x => x.HistoriaClinica != null &&
                                      x.HistoriaClinica.Paciente != null &&
                                      (x.HistoriaClinica.Paciente.Cedula != null &&
                                       x.HistoriaClinica.Paciente.Cedula.Contains(entidad.HistoriaClinica!.Paciente!.Cedula!));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion!)) ||
                                      (x.Fecha == entidad.Fecha) ||
                                      (x.Medico != null && x.Medico.Nombre != null && x.Medico.Nombre.Contains(entidad.Medico!.Nombre!)) ||
                                      (x.HistoriaClinica != null && x.HistoriaClinica.Paciente != null &&
                                       x.HistoriaClinica.Paciente.Nombre != null &&
                                       x.HistoriaClinica.Paciente.Nombre.Contains(entidad.HistoriaClinica!.Paciente!.Nombre!)) ||
                                      (x.HistoriaClinica != null && x.HistoriaClinica.Paciente != null &&
                                       x.HistoriaClinica.Paciente.Cedula != null &&
                                       x.HistoriaClinica.Paciente.Cedula.Contains(entidad.HistoriaClinica!.Paciente!.Cedula!));
                    break;

                default:
                    condiciones = x => x.IdDiagnostico == entidad.IdDiagnostico;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Diagnostico Modificar(Diagnostico entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdDiagnostico == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
