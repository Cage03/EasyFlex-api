using Interface.Models;

namespace Interface.Dtos;

public class FlexWorker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public virtual ICollection<Skill> Skills { get; set; }

    public FlexWorker(FlexworkerModel flexworkerModel)
    {
        Id = flexworkerModel.Id;
        Name = flexworkerModel.Name;
        Address = flexworkerModel.Address;
        DateOfBirth = flexworkerModel.DateOfBirth;
        Email = flexworkerModel.Email;
        PhoneNumber = flexworkerModel.PhoneNumber;
        ProfilePictureUrl = flexworkerModel.ProfilePictureUrl;
        Skills = flexworkerModel.Skills.Select(s => new Skill(s)).ToList();
    }
    public FlexworkerModel ToModel()
    {
        return new FlexworkerModel
        {
            Id = Id,
            Name = Name,
            Address = Address,
            DateOfBirth = DateOfBirth,
            Email = Email,
            PhoneNumber = PhoneNumber,
            ProfilePictureUrl = ProfilePictureUrl,
            Skills = Skills.Select(s => s.ToModel()).ToList()
        };
    }
}