using Interface.Models;

namespace Logic.Classes;

public class FlexWorker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public virtual ICollection<Job> Jobs { get; set; }
    public virtual ICollection<Skill> Skills { get; set; }
    
    public FlexWorker(FlexworkerModel flexworkerModel)
    {
        Id = flexworkerModel.Id;
        Name = flexworkerModel.Name;
        Adress = flexworkerModel.Adress;
        DateOfBirth = flexworkerModel.DateOfBirth;
        Email = flexworkerModel.Email;
        PhoneNumber = flexworkerModel.PhoneNumber;
        ProfilePictureUrl = flexworkerModel.ProfilePictureUrl;
        Jobs = flexworkerModel.Jobs.Select(j => new Job(j)).ToList();
        Skills = flexworkerModel.Skills.Select(s => new Skill(s)).ToList();
    }
    public FlexworkerModel ToModel()
    {
        return new FlexworkerModel
        {
            Id = Id,
            Name = Name,
            Adress = Adress,
            DateOfBirth = DateOfBirth,
            Email = Email,
            PhoneNumber = PhoneNumber,
            ProfilePictureUrl = ProfilePictureUrl,
            Jobs = Jobs.Select(j => j.ToModel()).ToList(),
            Skills = Skills.Select(s => s.ToModel()).ToList()
        };
    }
}