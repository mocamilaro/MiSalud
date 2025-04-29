using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface ITratamientoAplicacion
    {
        void Configurar(string string_conexion);
        List<Tratamiento> Buscar(Tratamiento entidad, string tipo);
        List<Tratamiento> Listar();
        Tratamiento Guardar(Tratamiento entidad);
        Tratamiento Modificar(Tratamiento entidad);
        Tratamiento Borrar(Tratamiento entidad);
    }
}