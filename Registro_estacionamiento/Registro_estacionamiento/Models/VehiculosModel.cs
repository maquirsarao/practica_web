using Microsoft.Win32;

namespace Registro_estacionamiento.Models
{
    public class VehiculosModel
    {
        public int Id { get; set; }
        public string NumeroDePlaca { get; set; }
        public ICollection<RegistrosModel> RegistrosModel { get; set; }
    }
}
