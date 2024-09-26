using Interface.Models;

namespace Logic.Classes;

public class Skill
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<FlexWorker> Flexworkers { get; set; }
    public virtual ICollection<Job> Jobs { get; set; }

    public Skill(SkillModel skillModel)
    {
        Id = skillModel.Id;
        CategoryId = skillModel.CategoryId;
        Name = skillModel.Name;
        Category = new Category(skillModel.CategoryModel);
        Flexworkers = skillModel.Flexworkers.Select(f => new FlexWorker(f)).ToList();
        Jobs = skillModel.Jobs.Select(j => new Job(j)).ToList();
    }
    public SkillModel ToModel()
    {
        return new SkillModel
        {
            Id = Id,
            CategoryId = CategoryId,
            Name = Name,
            CategoryModel = Category.ToModel(),
            Flexworkers = Flexworkers.Select(f => f.ToModel()).ToList(),
            Jobs = Jobs.Select(j => j.ToModel()).ToList()
        };
    }
}