using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IPacientePresentacion
    {
        Task<List<Paciente>> Listar();
        Task<List<Paciente>> Buscar(Paciente entidad, string tipo);
        Task<Paciente> Guardar(Paciente entidad);
        Task<Paciente> Modificar(Paciente entidad);
        Task<Paciente> Borrar(Paciente entidad);
    }
}