using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class PacienteRepositorio : IPacienteRepositorio
    {
        private Conexion? conexion = null;

        public PacienteRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Paciente> Listar()
        {
            return conexion!.Listar<Paciente>();
        }
        public List<Paciente> Buscar(Expression<Func<Paciente, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Paciente Guardar(Paciente entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Paciente Modificar(Paciente entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Paciente Borrar(Paciente entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}