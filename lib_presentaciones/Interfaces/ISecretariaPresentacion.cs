using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface ISecretariaPresentacion
    {
        Task<List<Secretaria>> Listar();
        Task<List<Secretaria>> Buscar(Secretaria entidad, string tipo);
        Task<Secretaria> Guardar(Secretaria entidad);
        Task<Secretaria> Modificar(Secretaria entidad);
        Task<Secretaria> Borrar(Secretaria entidad);
    }
}