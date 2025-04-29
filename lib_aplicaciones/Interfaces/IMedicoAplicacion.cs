using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IMedicoAplicacion
    {
        void Configurar(string string_conexion);
        List<Medico> Buscar(Medico entidad, string tipo);
        List<Medico> Listar();
        Medico Guardar(Medico entidad);
        Medico Modificar(Medico entidad);
        Medico Borrar(Medico entidad);
    }
}