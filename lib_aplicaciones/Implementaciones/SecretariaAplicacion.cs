using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class SecretariaAplicacion : ISecretariaAplicacion
    {
        private ISecretariaRepositorio? iRepositorio = null;

        public SecretariaAplicacion(ISecretariaRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Secretaria Borrar(Secretaria entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Secretaria Guardar(Secretaria entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Secretaria> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Secretaria> Buscar(Secretaria entidad, string tipo)
        {
            Expression<Func<Secretaria, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "NOMBRE":
                    condiciones = x => x.Nombre != null &&
                                     x.Nombre.Contains(entidad.Nombre!);
                    break;

                case "CEDULA":
                    condiciones = x => x.Cedula != null &&
                                     x.Cedula.Contains(entidad.Cedula!);
                    break;

                case "EMAIL":
                    condiciones = x => x.Email != null &&
                                     x.Email.Contains(entidad.Email!);
                    break;

                case "TELEFONO":
                    condiciones = x => x.Telefono != null &&
                                     x.Telefono.Contains(entidad.Telefono!);
                    break;

                case "CITAS_ASIGNADAS":
                    condiciones = x => x.CitasPendientes != null &&
                                     x.CitasPendientes.Any(c =>
                                         c.Estado == EstadoCita.Pendiente ||
                                         c.Estado == EstadoCita.Asignada);
                    break;

                case "CITAS_RECIENTES":
                    condiciones = x => x.CitasPendientes != null &&
                                     x.CitasPendientes.Any(c =>
                                         c.Fecha >= DateTime.Now.AddDays(-7));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) ||
                                      (x.Cedula != null && x.Cedula.Contains(entidad.Cedula!)) ||
                                      (x.Email != null && x.Email.Contains(entidad.Email!)) ||
                                      (x.Telefono != null && x.Telefono.Contains(entidad.Telefono!)) ||
                                      (x.CitasPendientes != null && x.CitasPendientes.Any(c =>
                                          c.Estado == EstadoCita.Pendiente ||
                                          c.Estado == EstadoCita.Asignada)) ||
                                      (x.CitasPendientes != null && x.CitasPendientes.Any(c =>
                                          c.Fecha >= DateTime.Now.AddDays(-7)));
                    break;

                default:
                    condiciones = x => x.Id == entidad.Id;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Secretaria Modificar(Secretaria entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
