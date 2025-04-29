using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface ITratamientoPresentacion
    {
        Task<List<Tratamiento>> Listar();
        Task<List<Tratamiento>> Buscar(Tratamiento entidad, string tipo);
        Task<Tratamiento> Guardar(Tratamiento entidad);
        Task<Tratamiento> Modificar(Tratamiento entidad);
        Task<Tratamiento> Borrar(Tratamiento entidad);
    }
}