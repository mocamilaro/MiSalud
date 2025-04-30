using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class PacienteAplicacion : IPacienteAplicacion
    {
        private IPacienteRepositorio? iRepositorio = null;

        public PacienteAplicacion(IPacienteRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Paciente Borrar(Paciente entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Paciente Guardar(Paciente entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Paciente> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Paciente> Buscar(Paciente entidad, string tipo)
        {
            Expression<Func<Paciente, bool>>? condiciones = null;

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

                case "HISTORIA_CLINICA":
                    condiciones = x => x.HistoriaClinica != null &&
                                     (x.HistoriaClinica.Diagnosticos != null &&
                                      x.HistoriaClinica.Diagnosticos.Any(d =>
                                          d.Descripcion != null &&
                                          d.Descripcion.Contains(entidad.HistoriaClinica!.Diagnosticos!.First().Descripcion!)));
                    break;

                case "MEDICO_TRATANTE":
                    condiciones = x => x.Citas != null &&
                                     x.Citas.Any(c =>
                                         c.Medico != null &&
                                         (c.Medico.Nombre != null &&
                                          c.Medico.Nombre.Contains(entidad.Citas!.First().Medico!.Nombre!)));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) ||
                                      (x.Cedula != null && x.Cedula.Contains(entidad.Cedula!)) ||
                                      (x.Email != null && x.Email.Contains(entidad.Email!));

                    break;

                default:
                    condiciones = x => x.Id == entidad.Id;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Paciente Modificar(Paciente entidad)
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
