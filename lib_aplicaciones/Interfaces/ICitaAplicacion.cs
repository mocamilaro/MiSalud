using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface ICitaAplicacion
    {
        void Configurar(string string_conexion);
        List<Cita> Buscar(Cita entidad, string tipo);
        List<Cita> Listar();
        Cita Guardar(Cita entidad);
        Cita Modificar(Cita entidad);
        Cita Borrar(Cita entidad);
    }
}