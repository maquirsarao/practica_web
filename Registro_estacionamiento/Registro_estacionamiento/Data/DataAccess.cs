using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Registro_estacionamiento.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<T>(sql, param);
            }
        }

        public T QuerySingle<T>(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.QuerySingle<T>(sql, param);
            }
        }

        public void Execute(string sql, object param = null)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute(sql, param);
            }
        }
    }
}
