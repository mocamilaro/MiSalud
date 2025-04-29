using lib_entidades.Modelos;

namespace lib_aplicaciones.Interfaces
{
    public interface IHistoriaClinicaAplicacion
    {
        void Configurar(string string_conexion);
        List<HistoriaClinica> Buscar(HistoriaClinica entidad, string tipo);
        List<HistoriaClinica> Listar();
        HistoriaClinica Guardar(HistoriaClinica entidad);
        HistoriaClinica Modificar(HistoriaClinica entidad);
        HistoriaClinica Borrar(HistoriaClinica entidad);
    }
}