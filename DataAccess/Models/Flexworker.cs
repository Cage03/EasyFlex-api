using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Flexworker
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Adress { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
