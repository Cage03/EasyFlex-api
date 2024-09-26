namespace Logic.Classes;

public class Skill
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<Flexworker> Flexworkers { get; set; }
    public virtual ICollection<Job> Jobs { get; set; }
}