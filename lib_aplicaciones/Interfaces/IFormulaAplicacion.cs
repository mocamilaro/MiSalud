using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IFormulaAplicacion
    {
        void Configurar(string string_conexion);
        List<Formula> Buscar(Formula entidad, string tipo);
        List<Formula> Listar();
        Formula Guardar(Formula entidad);
        Formula Modificar(Formula entidad);
        Formula Borrar(Formula entidad);
    }
}