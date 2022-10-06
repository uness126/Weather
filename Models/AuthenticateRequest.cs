using System.ComponentModel.DataAnnotations;

namespace Weather.Models;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}
