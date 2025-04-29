using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class RecomendacionAplicacion : IRecomendacionAplicacion
    {
        private IRecomendacionRepositorio? iRepositorio = null;

        public RecomendacionAplicacion(IRecomendacionRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Recomendacion Borrar(Recomendacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdRecomendacion == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Recomendacion Guardar(Recomendacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdRecomendacion != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Recomendacion> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Recomendacion> Buscar(Recomendacion entidad, string tipo)
        {
            Expression<Func<Recomendacion, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "TIPO":
                    condiciones = x => x.Tipo != null && x.Tipo.Contains(entidad.Tipo);
                    break;

                case "DESCRIPCION":
                    condiciones = x => x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion);
                    break;

                case "FECHA":
                    condiciones = x => x.FechaEmision.Date == entidad.FechaEmision.Date;
                    break;

                case "PACIENTE":
                    condiciones = x => x.PacienteId == entidad.PacienteId;
                    break;

                case "COMPLEJA":
                    condiciones = x =>
                        (x.Tipo != null && x.Tipo.Contains(entidad.Tipo)) ||
                        (x.Descripcion != null && x.Descripcion.Contains(entidad.Descripcion)) ||
                        (x.FechaEmision.Date == entidad.FechaEmision.Date) ||
                        (x.PacienteId == entidad.PacienteId);
                    break;

                default:
                    condiciones = x => x.IdRecomendacion == entidad.IdRecomendacion;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Recomendacion Modificar(Recomendacion entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdRecomendacion == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
