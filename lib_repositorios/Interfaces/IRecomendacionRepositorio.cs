using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IRecomendacionRepositorio
    {
        void Configurar(string string_conexion);
        List<Recomendacion> Listar();
        List<Recomendacion> Buscar(Expression<Func<Recomendacion, bool>> condiciones);
        Recomendacion Guardar(Recomendacion entidad);
        Recomendacion Modificar(Recomendacion entidad);
        Recomendacion Borrar(Recomendacion entidad);

    }
}
