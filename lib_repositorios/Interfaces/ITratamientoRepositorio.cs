using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface ITratamientoRepositorio
    {
        void Configurar(string string_conexion);
        List<Tratamiento> Listar();
        List<Tratamiento> Buscar(Expression<Func<Tratamiento, bool>> condiciones);
        Tratamiento Guardar(Tratamiento entidad);
        Tratamiento Modificar(Tratamiento entidad);
        Tratamiento Borrar(Tratamiento entidad);

    }
}
