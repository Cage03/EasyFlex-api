using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Job
{
    public int Id { get; set; }

    public string Adress { get; set; } = null!;

    public string? Description { get; set; }

    public int MinHours { get; set; }

    public int MaxHours { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual ICollection<Flexworker> Flexworkers { get; set; } = new List<Flexworker>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
