using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IMedicoPresentacion
    {
        Task<List<Medico>> Listar();
        Task<List<Medico>> Buscar(Medico entidad, string tipo);
        Task<Medico> Guardar(Medico entidad);
        Task<Medico> Modificar(Medico entidad);
        Task<Medico> Borrar(Medico entidad);
    }
}