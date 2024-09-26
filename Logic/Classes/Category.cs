namespace Logic.Classes;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Skill> Skills { get; set; }
}