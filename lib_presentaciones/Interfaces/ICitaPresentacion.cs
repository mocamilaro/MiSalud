using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface ICitaPresentacion
    {
        Task<List<Cita>> Listar();
        Task<List<Cita>> Buscar(Cita entidad, string tipo);
        Task<Cita> Guardar(Cita entidad);
        Task<Cita> Modificar(Cita entidad);
        Task<Cita> Borrar(Cita entidad);
    }
}