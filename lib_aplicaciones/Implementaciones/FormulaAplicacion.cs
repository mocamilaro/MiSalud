using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class FormulaAplicacion : IFormulaAplicacion
    {
        private IFormulaRepositorio? iRepositorio = null;

        public FormulaAplicacion(IFormulaRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Formula Borrar(Formula entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdFormula == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Formula Guardar(Formula entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdFormula != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Formula> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Formula> Buscar(Formula entidad, string tipo)
        {
            Expression<Func<Formula, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "FECHA":
                    condiciones = x => x.FechaCreacion == entidad.FechaCreacion;
                    break;

                case "PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                     (x.Paciente.Nombre != null && x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!));
                    break;

                case "MEDICO":
                    // Asumiendo que Formula tiene relación con Médico a través de Diagnostico o directamente
                    condiciones = x => x.HistoriaClinica != null &&
                                     x.HistoriaClinica.Diagnosticos.Any(d =>
                                         d.Medico != null &&
                                         d.Medico.Nombre != null &&
                                         d.Medico.Nombre.Contains(entidad.HistoriaClinica!.Diagnosticos.First().Medico!.Nombre!));
                    break;

                case "MEDICAMENTO":
                    condiciones = x => x.Medicamentos != null &&
                                     x.Medicamentos.Any(m =>
                                         m.Nombre != null &&
                                         m.Nombre.Contains(entidad.Medicamentos!.First().Nombre!));
                    break;

                case "CEDULA_PACIENTE":
                    condiciones = x => x.Paciente != null &&
                                     (x.Paciente.Cedula != null && x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.FechaCreacion == entidad.FechaCreacion) ||
                                      (x.Paciente != null && x.Paciente.Nombre != null && x.Paciente.Nombre.Contains(entidad.Paciente!.Nombre!)) ||
                                      (x.HistoriaClinica != null && x.HistoriaClinica.Diagnosticos.Any(d =>
                                          d.Medico != null &&
                                          d.Medico.Nombre != null &&
                                          d.Medico.Nombre.Contains(entidad.HistoriaClinica!.Diagnosticos.First().Medico!.Nombre!))) ||
                                      (x.Medicamentos != null && x.Medicamentos.Any(m =>
                                          m.Nombre != null &&
                                          m.Nombre.Contains(entidad.Medicamentos!.First().Nombre!))) ||
                                      (x.Paciente != null && x.Paciente.Cedula != null && x.Paciente.Cedula.Contains(entidad.Paciente!.Cedula!));
                    break;

                default:
                    condiciones = x => x.IdFormula == entidad.IdFormula;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Formula Modificar(Formula entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdFormula == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
