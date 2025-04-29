using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IMedicoRepositorio
    {
        void Configurar(string string_conexion);
        List<Medico> Listar();
        List<Medico> Buscar(Expression<Func<Medico, bool>> condiciones);
        Medico Guardar(Medico entidad);
        Medico Modificar(Medico entidad);
        Medico Borrar(Medico entidad);

    }
}
