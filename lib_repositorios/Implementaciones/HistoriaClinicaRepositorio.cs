using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class HistoriaClinicaRepositorio : IHistoriaClinicaRepositorio
    {
        private Conexion? conexion = null;

        public HistoriaClinicaRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<HistoriaClinica> Listar()
        {
            return conexion!.Listar<HistoriaClinica>();
        }
        public List<HistoriaClinica> Buscar(Expression<Func<HistoriaClinica, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }

        public List<HistoriaClinica> BuscarHistorias(Expression<Func<HistoriaClinica, bool>> condiciones)
        {
            return conexion!.BuscarHistorias(condiciones);
        }
        public HistoriaClinica Guardar(HistoriaClinica entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public HistoriaClinica Modificar(HistoriaClinica entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public HistoriaClinica Borrar(HistoriaClinica entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

    }
}