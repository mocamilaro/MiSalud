using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface ICitaRepositorio
    {
        void Configurar(string string_conexion);
        List<Cita> Listar();
        List<Cita> Buscar(Expression<Func<Cita, bool>> condiciones);
        Cita Guardar(Cita entidad);
        Cita Modificar(Cita entidad);
        Cita Borrar(Cita entidad);

        bool ExisteCitaEnHorario(int medicoId, DateTime fecha, TimeSpan hora);

    }
}
