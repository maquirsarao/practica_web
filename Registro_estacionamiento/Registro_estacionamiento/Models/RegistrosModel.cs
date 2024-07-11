namespace Registro_estacionamiento.Models
{
    public class RegistrosModel
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaHoraEntrada { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public decimal CostoPorHora { get; set; }
        public decimal? CostoTotal { get; set; }

        public VehiculosModel VehiculosModel { get; set; }
    }
}
