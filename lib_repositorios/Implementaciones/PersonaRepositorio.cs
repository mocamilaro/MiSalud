using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private Conexion? conexion = null;

        public PersonaRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Persona> Listar()
        {
            return conexion!.Listar<Persona>();
        }
        public List<Persona> Buscar(Expression<Func<Persona, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Persona Guardar(Persona entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Persona Modificar(Persona entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Persona Borrar(Persona entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}