using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IFormulaRepositorio
    {
        void Configurar(string string_conexion);
        List<Formula> Listar();
        List<Formula> Buscar(Expression<Func<Formula, bool>> condiciones);
        Formula Guardar(Formula entidad);
        Formula Modificar(Formula entidad);
        Formula Borrar(Formula entidad);

    }
}
