using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IPersonaPresentacion
    {
        Task<List<Persona>> Listar();
        Task<List<Persona>> Buscar(Persona entidad, string tipo);
        Task<Persona> Guardar(Persona entidad);
        Task<Persona> Modificar(Persona entidad);
        Task<Persona> Borrar(Persona entidad);
    }
}