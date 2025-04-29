using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IMedicamentoAplicacion
    {
        void Configurar(string string_conexion);
        List<Medicamento> Buscar(Medicamento entidad, string tipo);
        List<Medicamento> Listar();
        Medicamento Guardar(Medicamento entidad);
        Medicamento Modificar(Medicamento entidad);
        Medicamento Borrar(Medicamento entidad);
    }
}