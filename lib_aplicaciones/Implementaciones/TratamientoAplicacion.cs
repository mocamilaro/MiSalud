using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class TratamientoAplicacion : ITratamientoAplicacion
    {
        private ITratamientoRepositorio? iRepositorio = null;

        public TratamientoAplicacion(ITratamientoRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Tratamiento Borrar(Tratamiento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdTratamiento == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Tratamiento Guardar(Tratamiento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdTratamiento != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Tratamiento> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Tratamiento> Buscar(Tratamiento entidad, string tipo)
        {
            Expression<Func<Tratamiento, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "DESCRIPCION":
                    condiciones = x => x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion);
                    break;

                case "FECHA_INICIO":
                    condiciones = x => x.FechaInicio.Date == entidad.FechaInicio.Date;
                    break;

                case "FECHA_FIN":
                    condiciones = x => x.FechaFin != null && x.FechaFin.Value.Date == entidad.FechaFin!.Value.Date;
                    break;

                case "HISTORIA":
                    condiciones = x => x.HistoriaId == entidad.HistoriaId;
                    break;

                case "COMPLEJA":
                    condiciones = x =>
                        (x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion)) ||
                        (x.FechaInicio.Date == entidad.FechaInicio.Date) ||
                        (x.FechaFin != null && x.FechaFin.Value.Date == entidad.FechaFin!.Value.Date) ||
                        (x.HistoriaId == entidad.HistoriaId);
                    break;

                default:
                    condiciones = x => x.IdTratamiento == entidad.IdTratamiento;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Tratamiento Modificar(Tratamiento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdTratamiento == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
