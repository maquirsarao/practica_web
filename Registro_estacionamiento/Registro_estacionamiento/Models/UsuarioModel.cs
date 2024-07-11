using System.ComponentModel.DataAnnotations;

namespace Registro_estacionamiento.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100)]
        public string Contrasena { get; set; }

        public bool Activo { get; set; }
    }
}
