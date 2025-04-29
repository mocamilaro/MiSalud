using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class NotificacionRepositorio : INotificacionRepositorio
    {
        private Conexion? conexion = null;

        public NotificacionRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Notificacion> Listar()
        {
            return conexion!.Listar<Notificacion>();
        }
        public List<Notificacion> Buscar(Expression<Func<Notificacion, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Notificacion Guardar(Notificacion entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Notificacion Modificar(Notificacion entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Notificacion Borrar(Notificacion entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}