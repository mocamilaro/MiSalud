using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IPersonaRepositorio
    {
        void Configurar(string string_conexion);
        List<Persona> Listar();
        List<Persona> Buscar(Expression<Func<Persona, bool>> condiciones);
        Persona Guardar(Persona entidad);
        Persona Modificar(Persona entidad);
        Persona Borrar(Persona entidad);

    }
}
