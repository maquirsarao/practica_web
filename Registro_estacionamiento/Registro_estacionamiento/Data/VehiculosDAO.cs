using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Registro_estacionamiento.Models;
using System.Data;

namespace Registro_estacionamiento.Data
{
    public class VehiculosDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly DataAccess _dataAccess;
        private readonly DbConnectionFactory _connectionFactory;

        public VehiculosDAO(ApplicationDbContext context, DataAccess dataAccess, DbConnectionFactory connectionFactory)
        {
            _context = context;
            _dataAccess = dataAccess;
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<VehiculosModel> GetAllVehiculos()
        {
            return _context.Vehiculos.ToList();
        }

        public VehiculosModel GetVehiculoById(int id)
        {
            return _context.Vehiculos.Find(id);
        }
        public VehiculosModel GetVehiculoPlaca(string numeroDePlaca)
        {
            using (var connection = _connectionFactory.GetDbConnection())
            {
                string query = "SELECT TOP 1 Id, NumeroDePlaca FROM Vehiculos WHERE NumeroDePlaca = @NumeroDePlaca";
                return connection.QueryFirstOrDefault<VehiculosModel>(query, new { NumeroDePlaca = numeroDePlaca });
            }
        }
        public int agregarVehiculoyRegistro(VehiculosModel vehiculo,RegistrosModel registro)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@NumeroDePlaca", vehiculo.NumeroDePlaca);
            parameters.Add("@FechaHoraEntrada", registro.FechaHoraEntrada);
            parameters.Add("@CostoPorHora", registro.CostoPorHora);
            parameters.Add("@VehiculoId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            _dataAccess.ExecuteStoredProcedure("RegistroEntradaVehiculo", parameters);

            return parameters.Get<int>("@VehiculoId");
        }
        public void AddVehiculo(VehiculosModel vehiculo)
        {
            _context.Vehiculos.Add(vehiculo);
            _context.SaveChanges();
        }

        public void UpdateVehiculo(VehiculosModel vehiculo)
        {
            _context.Entry(vehiculo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteVehiculo(int id)
        {
            var vehiculo = _context.Vehiculos.Find(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                _context.SaveChanges();
            }
        }
    }
}
