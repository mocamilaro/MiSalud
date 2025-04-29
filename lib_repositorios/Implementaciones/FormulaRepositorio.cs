using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class FormulaRepositorio : IFormulaRepositorio
    {
        private Conexion? conexion = null;

        public FormulaRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Formula> Listar()
        {
            return conexion!.Listar<Formula>();
        }
        public List<Formula> Buscar(Expression<Func<Formula, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Formula Guardar(Formula entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Formula Modificar(Formula entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Formula Borrar(Formula entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}