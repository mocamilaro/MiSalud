using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IPacienteAplicacion
    {
        void Configurar(string string_conexion);
        List<Paciente> Buscar(Paciente entidad, string tipo);
        List<Paciente> Listar();
        Paciente Guardar(Paciente entidad);
        Paciente Modificar(Paciente entidad);
        Paciente Borrar(Paciente entidad);
    }
}