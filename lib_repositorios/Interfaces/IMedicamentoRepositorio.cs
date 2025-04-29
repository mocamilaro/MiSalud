using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IMedicamentoRepositorio
    {
        void Configurar(string string_conexion);
        List<Medicamento> Listar();
        List<Medicamento> Buscar(Expression<Func<Medicamento, bool>> condiciones);
        Medicamento Guardar(Medicamento entidad);
        Medicamento Modificar(Medicamento entidad);
        Medicamento Borrar(Medicamento entidad);

    }
}
