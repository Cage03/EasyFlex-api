namespace Interface.Models;

public partial class FlexworkerModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Adress { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public virtual ICollection<JobModel> Jobs { get; set; } = new List<JobModel>();
    public virtual ICollection<SkillModel> Skills { get; set; } = new List<SkillModel>();
    
    // public FlexWorkerDto ToDto()
    // {
    //     return new FlexWorkerDto
    //     {
    //         Id = Id,
    //         Name = Name,
    //         Adress = Adress,
    //         DateOfBirth = DateOfBirth,
    //         Email = Email,
    //         PhoneNumber = PhoneNumber,
    //         ProfilePictureUrl = ProfilePictureUrl,
    //         Jobs = Jobs.Select(j => j.ToDto()).ToList(),
    //         Skills = Skills.Select(s => s.ToDto()).ToList()
    //     };
    // }
}
