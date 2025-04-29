using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class SecretariaRepositorio : ISecretariaRepositorio
    {
        private Conexion? conexion = null;

        public SecretariaRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Secretaria> Listar()
        {
            return conexion!.Listar<Secretaria>();
        }
        public List<Secretaria> Buscar(Expression<Func<Secretaria, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Secretaria Guardar(Secretaria entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Secretaria Modificar(Secretaria entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Secretaria Borrar(Secretaria entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}