using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface ISecretariaAplicacion
    {
        void Configurar(string string_conexion);
        List<Secretaria> Buscar(Secretaria entidad, string tipo);
        List<Secretaria> Listar();
        Secretaria Guardar(Secretaria entidad);
        Secretaria Modificar(Secretaria entidad);
        Secretaria Borrar(Secretaria entidad);
    }
}