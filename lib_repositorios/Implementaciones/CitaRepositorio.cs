using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class CitaRepositorio : ICitaRepositorio
    {
        private Conexion? conexion = null;

        public CitaRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Cita> Listar()
        {
            return conexion!.Listar<Cita>();
        }
        public List<Cita> Buscar(Expression<Func<Cita, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Cita Guardar(Cita entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Cita Modificar(Cita entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Cita Borrar(Cita entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public bool ExisteCitaEnHorario(int medicoId, DateTime fecha, TimeSpan hora)
        {
            return conexion!.Citas
                .Any(c => c.MedicoId == medicoId &&
                          c.Fecha.Date == fecha.Date &&
                          c.Hora == hora &&
                          c.Estado != EstadoCita.Cancelada); // Opcional: excluir canceladas
        }
    }
}