using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IHistoriaClinicaPresentacion
    {
        Task<List<HistoriaClinica>> Listar();
        Task<List<HistoriaClinica>> Buscar(HistoriaClinica entidad, string tipo);
        Task<HistoriaClinica> Guardar(HistoriaClinica entidad);
        Task<HistoriaClinica> Modificar(HistoriaClinica entidad);
        Task<HistoriaClinica> Borrar(HistoriaClinica entidad);
    }
}