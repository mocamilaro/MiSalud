using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class RecomendacionRepositorio : IRecomendacionRepositorio
    {
        private Conexion? conexion = null;

        public RecomendacionRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Recomendacion> Listar()
        {
            return conexion!.Listar<Recomendacion>();
        }
        public List<Recomendacion> Buscar(Expression<Func<Recomendacion, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Recomendacion Guardar(Recomendacion entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Recomendacion Modificar(Recomendacion entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Recomendacion Borrar(Recomendacion entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}