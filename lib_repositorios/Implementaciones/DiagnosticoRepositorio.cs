using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class DiagnosticoRepositorio : IDiagnosticoRepositorio
    {
        private Conexion? conexion = null;

        public DiagnosticoRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Diagnostico> Listar()
        {
            return conexion!.Listar<Diagnostico>();
        }
        public List<Diagnostico> Buscar(Expression<Func<Diagnostico, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Diagnostico Guardar(Diagnostico entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Diagnostico Modificar(Diagnostico entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Diagnostico Borrar(Diagnostico entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}