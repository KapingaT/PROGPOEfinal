using System.ComponentModel.DataAnnotations;

namespace RacedayFINAL.Models
{
    public class RegistrationDTO
    {
        [Required] public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, MinLength(6), DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
        [Required] public string Role { get; set; } = "Participant";
    }
}
