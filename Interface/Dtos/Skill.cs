using Interface.Models;

namespace Interface.Dtos;

public class Skill
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public Skill(SkillModel skillModel)
    {
        Id = skillModel.Id;
        CategoryId = skillModel.CategoryId;
        Name = skillModel.Name;
    }
    public SkillModel ToModel()
    {
        return new SkillModel
        {
            Id = Id,
            CategoryId = CategoryId,
            Name = Name,
        };
    }
}