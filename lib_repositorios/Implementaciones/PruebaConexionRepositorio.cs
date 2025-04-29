using lib_repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_repositorios.Implementaciones
{
    public class PruebaConexionRepositorio : IPruebaConexionRepositorio
    {
        private readonly Conexion _contexto;

        public PruebaConexionRepositorio(Conexion contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ProbarConexionAsync()
        {
            try
            {
                return await _contexto.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}