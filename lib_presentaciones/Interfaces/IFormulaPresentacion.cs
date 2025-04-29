using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IFormulaPresentacion
    {
        Task<List<Formula>> Listar();
        Task<List<Formula>> Buscar(Formula entidad, string tipo);
        Task<Formula> Guardar(Formula entidad);
        Task<Formula> Modificar(Formula entidad);
        Task<Formula> Borrar(Formula entidad);
    }
}