using System.ComponentModel.DataAnnotations;

namespace Interface.Models;

public partial class FlexworkerModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public virtual ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();
}
