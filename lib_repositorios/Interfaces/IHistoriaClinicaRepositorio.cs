using lib_entidades.Modelos;
using System.Linq.Expressions;

namespace lib_repositorios.Interfaces
{
    public interface IHistoriaClinicaRepositorio
    {
        void Configurar(string string_conexion);
        List<HistoriaClinica> Listar();
        List<HistoriaClinica> Buscar(Expression<Func<HistoriaClinica, bool>> condiciones);
        List<HistoriaClinica> BuscarHistorias(Expression<Func<HistoriaClinica, bool>> condiciones); // <-- nuevo
        HistoriaClinica Guardar(HistoriaClinica entidad);
        HistoriaClinica Modificar(HistoriaClinica entidad);
        HistoriaClinica Borrar(HistoriaClinica entidad);

    }
}
