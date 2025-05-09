﻿using lib_entidades.Modelos;
using lib_repositorios.Interfaces;
using System.Linq.Expressions;

namespace lib_repositorios.Implementaciones
{
    public class TratamientoRepositorio : ITratamientoRepositorio
    {
        private Conexion? conexion = null;

        public TratamientoRepositorio(Conexion conexion)
        {
            this.conexion = conexion;
        }

        public void Configurar(string string_conexion)
        {
            this.conexion!.StringConnection = string_conexion;
        }

        public List<Tratamiento> Listar()
        {
            return conexion!.Listar<Tratamiento>();
        }
        public List<Tratamiento> Buscar(Expression<Func<Tratamiento, bool>> condiciones)
        {
            return conexion!.Buscar(condiciones);
        }
        public Tratamiento Guardar(Tratamiento entidad)
        {
            conexion!.Guardar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Tratamiento Modificar(Tratamiento entidad)
        {
            conexion!.Modificar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }

        public Tratamiento Borrar(Tratamiento entidad)
        {
            conexion!.Borrar(entidad);
            conexion!.GuardarCambios();
            return entidad;
        }
    }
}