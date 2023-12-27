using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

// #nullable disable
public class LoginDto
{
    [Required(ErrorMessage = "Username")]
    [MinLength(4,ErrorMessage ="pls enter more")]
    public required string? UserName { get; set; }
    [MinLength(4,ErrorMessage ="pls enter more")]
    public required string? Password { get; set; }
}
