using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class MedicamentoRepositorio : IMedicamentoRepositorio
    {
        private Conexion? conexion = null;

        public MedicamentoRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Medicamento> Listar()
        {
            return conexion!.Listar<Medicamento>();
        }
        public List<Medicamento> Buscar(Expression<Func<Medicamento, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Medicamento Guardar(Medicamento entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Medicamento Modificar(Medicamento entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Medicamento Borrar(Medicamento entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}