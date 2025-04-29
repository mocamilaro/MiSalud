using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class CitaAplicacion : ICitaAplicacion
    {
        private ICitaRepositorio? iRepositorio = null;

        public CitaAplicacion(ICitaRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Cita Borrar(Cita entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdCita == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Cita Guardar(Cita entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdCita != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Cita> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Cita> Buscar(Cita entidad, string tipo)
        {
            Expression<Func<Cita, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                     (x.Paciente.Nombre != null && x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!));
                    break;

                case "MEDICO":
                    condiciones = x => x.Medico != null &&
                                     (x.Medico.Nombre != null && x.Medico.Nombre.Contains(entidad.Medico!.Nombre!));
                    break;

                case "FECHA":
                    condiciones = x => x.Fecha == entidad.Fecha;
                    break;

                case "ESTADO":
                    condiciones = x => x.Estado == entidad.Estado;
                    break;

                case "CEDULA_PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                     (x.Paciente.Cedula != null && x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Paciente != null && x.Paciente.Nombre != null && x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!)) ||
                                      (x.Medico != null && x.Medico.Nombre != null && x.Medico.Nombre.Contains(entidad.Medico!.Nombre!)) ||
                                      (x.Fecha == entidad.Fecha) ||
                                      (x.Estado == entidad.Estado) ||
                                      (x.Paciente != null && x.Paciente.Cedula != null && x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!));
                    break;

                default:
                    condiciones = x => x.IdCita == entidad.IdCita;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Cita Modificar(Cita entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdCita == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }

        public Cita CrearCita(Cita cita)
        {
            // Validaciones básicas 
            if (cita == null)
                throw new Exception("La cita no puede ser nula.");
            if (cita.Fecha == default || cita.Fecha < DateTime.Today)
                throw new Exception("La fecha debe ser futura.");
            if (cita.Hora == default)
                throw new Exception("Seleccione una hora válida.");
            if (string.IsNullOrWhiteSpace(cita.Motivo))
                throw new Exception("Ingrese el motivo.");
            if (cita.PacienteId <= 0 || cita.MedicoId <= 0)
                throw new Exception("Paciente o médico inválido.");

            // Validar horario ocupado (usando CitaRepositorio)
            if (iRepositorio!.ExisteCitaEnHorario(cita.MedicoId, cita.Fecha, cita.Hora))
                throw new Exception("El médico ya tiene una cita en ese horario.");

            // Completar campos
            cita.Estado = EstadoCita.Asignada;
            cita.CreatedAt = DateTime.Now;
            cita.UpdatedAt = DateTime.Now;

            // Guardar 
            return iRepositorio.Guardar(cita);
        }

    }
}
