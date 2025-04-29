using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class NotificacionAplicacion : INotificacionAplicacion
    {
        private INotificacionRepositorio? iRepositorio = null;

        public NotificacionAplicacion(INotificacionRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Notificacion Borrar(Notificacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdNotificacion == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Notificacion Guardar(Notificacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdNotificacion != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Notificacion> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Notificacion> Buscar(Notificacion entidad, string tipo)
        {
            Expression<Func<Notificacion, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "TIPO":
                    condiciones = x => x.Tipo != null &&
                                     x.Tipo.Contains(entidad.Tipo!);
                    break;

                case "MENSAJE":
                    condiciones = x => x.Mensaje != null &&
                                     x.Mensaje.Contains(entidad.Mensaje!);
                    break;

                case "ESTADO":
                    condiciones = x => x.Estado != null &&
                                     x.Estado.Equals(entidad.Estado);
                    break;

                case "FECHA":
                    condiciones = x => x.FechaEnvio.Date == entidad.FechaEnvio.Date;
                    break;

                case "PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                     (x.Paciente.Nombre != null &&
                                      x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!));
                    break;

                case "CITA":
                    condiciones = x => x.Cita != null &&
                                     x.Cita.IdCita == entidad.Cita!.IdCita;
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Tipo != null && x.Tipo.Contains(entidad.Tipo!)) ||
                                      (x.Mensaje != null && x.Mensaje.Contains(entidad.Mensaje!)) ||
                                      (x.Estado != null && x.Estado.Equals(entidad.Estado)) ||
                                      (x.FechaEnvio.Date == entidad.FechaEnvio.Date) ||
                                      (x.Paciente != null && x.Paciente.Nombre != null &&
                                       x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!)) ||
                                      (x.Cita != null && x.Cita.IdCita == entidad.Cita!.IdCita);
                    break;

                default:
                    condiciones = x => x.IdNotificacion == entidad.IdNotificacion;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Notificacion Modificar(Notificacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdNotificacion == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
