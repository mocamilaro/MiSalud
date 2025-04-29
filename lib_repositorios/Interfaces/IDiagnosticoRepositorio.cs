using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IDiagnosticoRepositorio
    {
        void Configurar(string string_conexion);
        List<Diagnostico> Listar();
        List<Diagnostico> Buscar(Expression<Func<Diagnostico, bool>> condiciones);
        Diagnostico Guardar(Diagnostico entidad);
        Diagnostico Modificar(Diagnostico entidad);
        Diagnostico Borrar(Diagnostico entidad);

    }
}
