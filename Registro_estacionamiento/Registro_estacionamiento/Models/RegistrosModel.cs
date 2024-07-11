namespace Registro_estacionamiento.Models
{
    public class RegistrosModel
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public double CostoPorHora { get; set; }
        public double? CostoTotal { get; set; }

        public VehiculosModel VehiculosModel { get; set; }
    }
}
