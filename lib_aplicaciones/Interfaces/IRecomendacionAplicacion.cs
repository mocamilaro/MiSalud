using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IRecomendacionAplicacion
    {
        void Configurar(string string_conexion);
        List<Recomendacion> Buscar(Recomendacion entidad, string tipo);
        List<Recomendacion> Listar();
        Recomendacion Guardar(Recomendacion entidad);
        Recomendacion Modificar(Recomendacion entidad);
        Recomendacion Borrar(Recomendacion entidad);
    }
}