using Dapper;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.Data
{
    public class ParametrosDAO
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ParametrosDAO(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public ParametrosModel GetParametrosActivos(string parametro)
        {
            using (var connection = _connectionFactory.GetDbConnection())
            {
                return connection.QuerySingleOrDefault<ParametrosModel>("SELECT Id,Parametro,Valor,Estatus FROM Parametros where Parametro= @Parametro AND Estatus=1 ", new { Parametro = parametro });
            }
        }
    }
}
