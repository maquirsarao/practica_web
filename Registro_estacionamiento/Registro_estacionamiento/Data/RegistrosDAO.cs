using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.Data
{
    public class RegistrosDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly DbConnectionFactory _connectionFactory;

        public RegistrosDAO(ApplicationDbContext context, DbConnectionFactory connectionFactory)
        {
            _context = context;
            _connectionFactory = connectionFactory;
        }

        public IEnumerable<RegistrosModel> GetAllRegistros()
        {
            return _context.Registros.Include(r => r.VehiculosModel).ToList();
        }

        public RegistrosModel GetRegistroById(int id)
        {
            return _context.Registros.Find(id);
        }
        public RegistrosModel GetVehiculoPlaca(string numeroDePlaca)
        {
            using (var connection = _connectionFactory.GetDbConnection())
            {
                return connection.QuerySingleOrDefault<RegistrosModel>("SELECT r.Id, r.VehiculoId, r.FechaHoraEntrada, r.FechaHoraSalida, r.CostoPorHora, r.CostoTotal \r\n  FROM Registros r\r\n  INNER JOIN Vehiculos v on v.Id =r.VehiculoId\r\n  WHERE v.NumeroDePlaca = @NumeroDePlaca AND r.FechaHoraSalida IS NULL ", new { NumeroDePlaca = numeroDePlaca });
            }
            //return _context.Vehiculos.Find(numeroDePlaca);
        }
        public void AddRegistro(RegistrosModel registro)
        {
            _context.Registros.Add(registro);
            _context.SaveChanges();
        }

        public void UpdateRegistro(RegistrosModel registro)
        {
            _context.Entry(registro).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteRegistro(int id)
        {
            var registro = _context.Registros.Find(id);
            if (registro != null)
            {
                _context.Registros.Remove(registro);
                _context.SaveChanges();
            }
        }
    }
}
