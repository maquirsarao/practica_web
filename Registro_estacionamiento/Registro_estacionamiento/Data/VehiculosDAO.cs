using Microsoft.EntityFrameworkCore;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.Data
{
    public class VehiculosDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly DbConnectionFactory _connectionFactory;

        public VehiculosDAO(ApplicationDbContext context, DbConnectionFactory connectionFactory)
        {
            _context = context;
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
