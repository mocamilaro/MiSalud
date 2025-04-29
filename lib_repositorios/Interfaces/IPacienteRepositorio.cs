using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IPacienteRepositorio
    {
        void Configurar(string string_conexion);
        List<Paciente> Listar();
        List<Paciente> Buscar(Expression<Func<Paciente, bool>> condiciones);
        Paciente Guardar(Paciente entidad);
        Paciente Modificar(Paciente entidad);
        Paciente Borrar(Paciente entidad);

    }
}
