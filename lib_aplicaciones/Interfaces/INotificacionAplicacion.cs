using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface INotificacionAplicacion
    {
        void Configurar(string string_conexion);
        List<Notificacion> Buscar(Notificacion entidad, string tipo);
        List<Notificacion> Listar();
        Notificacion Guardar(Notificacion entidad);
        Notificacion Modificar(Notificacion entidad);
        Notificacion Borrar(Notificacion entidad);
    }
}