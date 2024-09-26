namespace Logic.Classes;

public class Flexworker
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
}