using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class UserModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public int Role { get; set; }
}
