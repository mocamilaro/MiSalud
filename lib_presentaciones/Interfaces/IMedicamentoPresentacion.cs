using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IMedicamentoPresentacion
    {
        Task<List<Medicamento>> Listar();
        Task<List<Medicamento>> Buscar(Medicamento entidad, string tipo);
        Task<Medicamento> Guardar(Medicamento entidad);
        Task<Medicamento> Modificar(Medicamento entidad);
        Task<Medicamento> Borrar(Medicamento entidad);
    }
}