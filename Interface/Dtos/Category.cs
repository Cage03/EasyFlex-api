using Interface.Models;

namespace Interface.Dtos;

public record Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Skill> Skills { get; set; }

    public Category(CategoryModel categoryModel)
    {
        Id = categoryModel.Id;
        Name = categoryModel.Name;
        Skills = categoryModel.Skills.Select(s => new Skill(s)).ToList();
    }
}