using Interface.Models;
using Logic.Classes;

namespace Logic.Dtos;

public class Category
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

    public CategoryModel ToModel()
    {
        return new CategoryModel
        {
            Id = Id,
            Name = Name,
            Skills = Skills.Select(s => s.ToModel()).ToList()
        };
    }
}