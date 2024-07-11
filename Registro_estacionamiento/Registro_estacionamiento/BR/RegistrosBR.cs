using Microsoft.Win32;
using Registro_estacionamiento.Data;
using Registro_estacionamiento.Models;

namespace Registro_estacionamiento.BR
{
    public class RegistrosBR
    {
        private readonly RegistrosDAO _registroDAO;
        private readonly ParametrosDAO _parametrosDAO;

        public RegistrosBR(RegistrosDAO registroDAO, ParametrosDAO parametrosDAO)
        {
            _registroDAO = registroDAO;
            _parametrosDAO = parametrosDAO;
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
        public double GetParametrosActivos(string parametro) {
            ParametrosModel param = _parametrosDAO.GetParametrosActivos(parametro);
            if(param != null) {
                return parametro != null ? Convert.ToDouble(param.Valor) : 0.0;
            }
            else
                return 0.0;
        }
    }
}
