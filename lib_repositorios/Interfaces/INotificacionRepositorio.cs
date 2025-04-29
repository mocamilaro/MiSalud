using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface INotificacionRepositorio
    {
        void Configurar(string string_conexion);
        List<Notificacion> Listar();
        List<Notificacion> Buscar(Expression<Func<Notificacion, bool>> condiciones);
        Notificacion Guardar(Notificacion entidad);
        Notificacion Modificar(Notificacion entidad);
        Notificacion Borrar(Notificacion entidad);

    }
}
