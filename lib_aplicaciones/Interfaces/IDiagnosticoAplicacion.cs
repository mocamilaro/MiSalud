using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IDiagnosticoAplicacion
    {
        void Configurar(string string_conexion);
        List<Diagnostico> Buscar(Diagnostico entidad, string tipo);
        List<Diagnostico> Listar();
        Diagnostico Guardar(Diagnostico entidad);
        Diagnostico Modificar(Diagnostico entidad);
        Diagnostico Borrar(Diagnostico entidad);
    }
}