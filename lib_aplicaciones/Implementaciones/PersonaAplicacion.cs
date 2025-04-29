using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class PersonaAplicacion : IPersonaAplicacion
    {
        private IPersonaRepositorio? iRepositorio = null;

        public PersonaAplicacion(IPersonaRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Persona Borrar(Persona entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Persona Guardar(Persona entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Persona> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Persona> Buscar(Persona entidad, string tipo)
        {
            Expression<Func<Persona, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "NOMBRE":
                    condiciones = x => x.Nombre != null &&
                                     x.Nombre.Contains(entidad.Nombre!);
                    break;

                case "CEDULA":
                    condiciones = x => x.Cedula != null &&
                                     x.Cedula.Contains(entidad.Cedula!);
                    break;

                case "EMAIL":
                    condiciones = x => x.Email != null &&
                                     x.Email.Contains(entidad.Email!);
                    break;

                case "TELEFONO":
                    condiciones = x => x.Telefono != null &&
                                     x.Telefono.Contains(entidad.Telefono!);
                    break;

                case "DIRECCION":
                    condiciones = x => x.Direccion != null &&
                                     x.Direccion.Contains(entidad.Direccion!);
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) ||
                                      (x.Cedula != null && x.Cedula.Contains(entidad.Cedula!)) ||
                                      (x.Email != null && x.Email.Contains(entidad.Email!)) ||
                                      (x.Telefono != null && x.Telefono.Contains(entidad.Telefono!)) ||
                                      (x.Direccion != null && x.Direccion.Contains(entidad.Direccion!));
                    break;

                default:
                    condiciones = x => x.Id == entidad.Id;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Persona Modificar(Persona entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
