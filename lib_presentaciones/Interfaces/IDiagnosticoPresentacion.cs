using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IDiagnosticoPresentacion
    {
        Task<List<Diagnostico>> Listar();
        Task<List<Diagnostico>> Buscar(Diagnostico entidad, string tipo);
        Task<Diagnostico> Guardar(Diagnostico entidad);
        Task<Diagnostico> Modificar(Diagnostico entidad);
        Task<Diagnostico> Borrar(Diagnostico entidad);
    }
}