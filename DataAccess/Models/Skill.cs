using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Skill
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Flexworker> Flexworkers { get; set; } = new List<Flexworker>();

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
