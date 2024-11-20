using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class FlexworkerModel
{
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Address { get; set; }

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string PhoneNumber { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public required ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();
}
