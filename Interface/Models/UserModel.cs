namespace Interface.Models;

public partial class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int Role { get; set; }
}
