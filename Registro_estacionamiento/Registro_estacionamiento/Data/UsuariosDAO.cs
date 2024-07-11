using Dapper;
using Microsoft.Data.SqlClient;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.Data
{
    public class UsuariosDAO
    {

        private readonly DbConnectionFactory _connectionFactory;

        public UsuariosDAO(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public UsuarioModel ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            using (var connection = _connectionFactory.GetDbConnection())
            {
                string query = "SELECT * FROM Usuarios WHERE Usuario = @NombreUsuario";
                return connection.QueryFirstOrDefault<UsuarioModel>(query, new { NombreUsuario = nombreUsuario });
            }
        }
    }
}
