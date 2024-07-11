using Microsoft.Win32;
using Registro_estacionamiento.Data;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.BR
{
    public class VehiculosBR
    {
        private readonly VehiculosDAO _vehiculosDAO;

        public VehiculosBR(VehiculosDAO vehiculosDAO)
        {
            _vehiculosDAO = vehiculosDAO;
        }

        public IEnumerable<VehiculosModel> ObtenerTodosLosVehiculos()
        {
            return _vehiculosDAO.GetAllVehiculos();
        }

        public VehiculosModel ObtenerVehiculoPorId(int id)
        {
            return _vehiculosDAO.GetVehiculoById(id);
        }

        public void AddVehiculo(VehiculosModel vehiculo)
        {
            _vehiculosDAO.AddVehiculo(vehiculo);
        }
        public void CrearVehiculo(VehiculosModel vehiculo)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _vehiculosDAO.AddVehiculo(vehiculo);
        }

        public void ActualizarVehiculo(VehiculosModel vehiculo)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _vehiculosDAO.UpdateVehiculo(vehiculo);
        }

        public void EliminarVehiculo(int id)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _vehiculosDAO.DeleteVehiculo(id);
        }
    }
}
