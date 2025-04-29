using lib_entidades;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;
using lib_entidades.Modelos;

namespace lib_aplicaciones.Implementaciones
{
    public class MedicamentoAplicacion : IMedicamentoAplicacion
    {
        private IMedicamentoRepositorio? iRepositorio = null;

        public MedicamentoAplicacion(IMedicamentoRepositorio iRepositorio)
        {
            this.iRepositorio = iRepositorio;
        }

        public void Configurar(string string_conexion)
        {
            this.iRepositorio!.Configurar(string_conexion);
        }

        public Medicamento Borrar(Medicamento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMedicamento == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Borrar(entidad);
            return entidad;
        }

        public Medicamento Guardar(Medicamento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMedicamento != 0)
                throw new Exception("lbYaSeGuardo");

            entidad = iRepositorio!.Guardar(entidad);
            return entidad;
        }

        public List<Medicamento> Listar()
        {
            return iRepositorio!.Listar();
        }

        public List<Medicamento> Buscar(Medicamento entidad, string tipo)
        {
            Expression<Func<Medicamento, bool>>? condiciones = null;

            switch (tipo.ToUpper())
            {
                case "NOMBRE":
                    condiciones = x => x.Nombre != null &&
                                     x.Nombre.Contains(entidad.Nombre!);
                    break;

                case "DOSIS":
                    condiciones = x => x.Dosis != null &&
                                     x.Dosis.Contains(entidad.Dosis!);
                    break;

                case "FRECUENCIA":
                    condiciones = x => x.Frecuencia != null &&
                                     x.Frecuencia.Contains(entidad.Frecuencia!);
                    break;

                case "FORMULA":
                    condiciones = x => x.Formula != null &&
                                     x.Formula.IdFormula == entidad.Formula!.IdFormula;
                    break;

                case "PACIENTE":
                    condiciones = x => x.Formula != null &&
                                     x.Formula.Paciente != null &&
                                     (x.Formula.Paciente.Nombre != null &&
                                      x.Formula.Paciente.Nombre.Contains(entidad.Formula!.Paciente!.Nombre!));
                    break;

                case "COMPLEJA":
                    condiciones = x => (x.Nombre != null && x.Nombre.Contains(entidad.Nombre!)) ||
                                      (x.Dosis != null && x.Dosis.Contains(entidad.Dosis!)) ||
                                      (x.Frecuencia != null && x.Frecuencia.Contains(entidad.Frecuencia!)) ||
                                      (x.Formula != null && x.Formula.IdFormula == entidad.Formula!.IdFormula) ||
                                      (x.Formula != null && x.Formula.Paciente != null &&
                                       x.Formula.Paciente.Nombre != null &&
                                       x.Formula.Paciente.Nombre.Contains(entidad.Formula!.Paciente!.Nombre!));
                    break;

                default:
                    condiciones = x => x.IdMedicamento == entidad.IdMedicamento;
                    break;
            }

            return this.iRepositorio!.Buscar(condiciones);
        }

        public Medicamento Modificar(Medicamento entidad)
        {
            if (entidad == null || !entidad.Validar())
                throw new Exception("lbFaltaInformacion");

            if (entidad.IdMedicamento == 0)
                throw new Exception("lbNoSeGuardo");

            entidad = iRepositorio!.Modificar(entidad);
            return entidad;
        }
    }
}
