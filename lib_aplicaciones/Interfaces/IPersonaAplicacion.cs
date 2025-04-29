using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IPersonaAplicacion
    {
        void Configurar(string string_conexion);
        List<Persona> Buscar(Persona entidad, string tipo);
        List<Persona> Listar();
        Persona Guardar(Persona entidad);
        Persona Modificar(Persona entidad);
        Persona Borrar(Persona entidad);
    }
}