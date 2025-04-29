using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class HistoriaClinicaAplicacion : IHistoriaClinicaAplicacion
    {
        private IHistoriaClinicaRepositorio? iRepositorio = null;

        public HistoriaClinicaAplicacion(IHistoriaClinicaRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public HistoriaClinica Borrar(HistoriaClinica entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdHistoria == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public HistoriaClinica Guardar(HistoriaClinica entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdHistoria != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<HistoriaClinica> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<HistoriaClinica> Buscar(HistoriaClinica entidad, string tipo)
        {
            Expression<Func<HistoriaClinica, bool>>? condiciones = null;



            switch (tipo.ToUpper())
            {
                case "PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                       x.Paciente.Nombre != null &&
                                       x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!);
                    break;

                case "FECHA_CREACION":
                    condiciones = x => x.FechaCreacion == entidad.FechaCreacion;
                    break;

                case "CEDULA_PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                       x.Paciente.Cedula != null &&
                                       x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!);
                    break;

                case "DIAGNOSTICO":
                    condiciones = x => x.Diagnosticos != null &&
                                       x.Diagnosticos.Any(d =>
                                           d.Descripcion != null &&
                                           d.Descripcion.Contains(entidad.Diagnosticos!.First().Descripcion!));
                    break;

                case "MEDICO":
                    condiciones = x => x.Diagnosticos != null &&
                                       x.Diagnosticos.Any(d =>
                                           d.Medico != null &&
                                           d.Medico.Nombre != null &&
                                           d.Medico.Nombre.Contains(entidad.Diagnosticos!.First().Medico!.Nombre!));
                    break;

                case "COMPLEJA":
                    condiciones = x =>
                        (x.Paciente != null && x.Paciente.Nombre != null && x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!)) ||
                        (x.FechaCreacion == entidad.FechaCreacion) ||
                        (x.Paciente != null && x.Paciente.Cedula != null && x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!)) ||
                        (x.Paciente != null && x.Paciente.Email != null && x.Paciente.Email.Contains(entidad.Paciente!.Email!));
                    break;

                default:
                    condiciones = x => x.IdHistoria == entidad.IdHistoria;
                    break;
            }

            return this.iRepositorio!.BuscarHistorias(condiciones);
        }

        public HistoriaClinica Modificar(HistoriaClinica entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdHistoria == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }

    }
}
