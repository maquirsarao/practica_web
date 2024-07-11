using Microsoft.Win32;
using Registro_estacionamiento.Data;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.BR
{
    public class RegistrosBR
    {
        private readonly RegistrosDAO _registroDAO;

        public RegistrosBR(RegistrosDAO registroDAO)
        {
            _registroDAO = registroDAO;
        }

        public IEnumerable<RegistrosModel> ObtenerTodosLosRegistros()
        {
            return _registroDAO.GetAllRegistros();
        }

        public RegistrosModel ObtenerRegistroPorId(int id)
        {
            return _registroDAO.GetRegistroById(id);
        }
        public RegistrosModel GetVehiculoPlaca(string numeroDePlaca)
        {
            return _registroDAO.GetVehiculoPlaca(numeroDePlaca);
        }
        public void CrearRegistro(RegistrosModel registro)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _registroDAO.AddRegistro(registro);
        }
        public void AddRegistro(RegistrosModel registro)
        {
            _registroDAO.AddRegistro(registro);
        }
        public void ActualizarRegistro(RegistrosModel registro)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _registroDAO.UpdateRegistro(registro);
        }

        public void EliminarRegistro(int id)
        {
            // Agrega cualquier lógica de negocio adicional aquí
            _registroDAO.DeleteRegistro(id);
        }
    }
}
