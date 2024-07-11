using Registro_estacionamiento.Data;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.BR
{
    public class UsuariosBR
    {
        private readonly UsuariosDAO _vehiculosDAO;

        public UsuariosBR(UsuariosDAO vehiculosDAO)
        {
            _vehiculosDAO = vehiculosDAO;
        }
        public UsuarioModel ObtenerUsuarioPorNombre(string nombreUsuario) {
            return _vehiculosDAO.ObtenerUsuarioPorNombre(nombreUsuario);
        }
    }
}
