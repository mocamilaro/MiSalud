using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class MedicoAplicacion : IMedicoAplicacion
    {
        private IMedicoRepositorio? iRepositorio = null;

        public MedicoAplicacion(IMedicoRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Medico Borrar(Medico entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Medico Guardar(Medico entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Medico> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Medico> Buscar(Medico entidad, string tipo)
        {
            Expression<Func<Medico, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "NOMBRE":
                    condiciones = x => x.Nombre != null &&
                                     x.Nombre.Contains(entidad.Nombre!);
                    break;

                case "ESPECIALIDAD":
                    condiciones = x => x.Especialidad != null &&
                                     x.Especialidad.Contains(entidad.Especialidad!);
                    break;

                case "CEDULA":
                    condiciones = x => x.Cedula != null &&
                                     x.Cedula.Contains(entidad.Cedula!);
                    break;

                case "EMAIL":
                    condiciones = x => x.Email != null &&
                                     x.Email.Contains(entidad.Email!);
                    break;

                case "PACIENTE_ATENDIDO":
                    condiciones = x => x.Citas != null &&
                                     x.Citas.Any(c =>
                                         c.Paciente != null &&
                                         c.Paciente.Nombre != null &&
                                         c.Paciente.Nombre.Contains(entidad.Citas!.First().Paciente!.Nombre!));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) ||
                                      (x.Especialidad != null && x.Especialidad.Contains(entidad.Especialidad!)) ||
                                      (x.Cedula != null && x.Cedula.Contains(entidad.Cedula!)) ||
                                      (x.Email != null && x.Email.Contains(entidad.Email!)) ||
                                      (x.Citas != null && x.Citas.Any(c =>
                                          c.Paciente != null &&
                                          c.Paciente.Nombre != null &&
                                          c.Paciente.Nombre.Contains(entidad.Citas!.First().Paciente!.Nombre!)));
                    break;

                default:
                    condiciones = x => x.Id == entidad.Id;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Medico Modificar(Medico entidad)
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
