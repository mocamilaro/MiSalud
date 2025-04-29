using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface INotificacionPresentacion
    {
        Task<List<Notificacion>> Listar();
        Task<List<Notificacion>> Buscar(Notificacion entidad, string tipo);
        Task<Notificacion> Guardar(Notificacion entidad);
        Task<Notificacion> Modificar(Notificacion entidad);
        Task<Notificacion> Borrar(Notificacion entidad);
    }
}