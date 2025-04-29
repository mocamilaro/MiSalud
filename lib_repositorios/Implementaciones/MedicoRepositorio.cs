using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class MedicoRepositorio : IMedicoRepositorio
    {
        private Conexion? conexion = null;

        public MedicoRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Medico> Listar()
        {
            return conexion!.Listar<Medico>();
        }
        public List<Medico> Buscar(Expression<Func<Medico, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Medico Guardar(Medico entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Medico Modificar(Medico entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Medico Borrar(Medico entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}