using lib_entidades.Modelos;

namespace lib_presentaciones.Interfaces
{
    public interface IRecomendacionPresentacion
    {
        Task<List<Recomendacion>> Listar();
        Task<List<Recomendacion>> Buscar(Recomendacion entidad, string tipo);
        Task<Recomendacion> Guardar(Recomendacion entidad);
        Task<Recomendacion> Modificar(Recomendacion entidad);
        Task<Recomendacion> Borrar(Recomendacion entidad);
    }
}