using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface ISecretariaRepositorio
    {
        void Configurar(string string_conexion);
        List<Secretaria> Listar();
        List<Secretaria> Buscar(Expression<Func<Secretaria, bool>> condiciones);
        Secretaria Guardar(Secretaria entidad);
        Secretaria Modificar(Secretaria entidad);
        Secretaria Borrar(Secretaria entidad);

    }
}
