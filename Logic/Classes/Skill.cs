using Interface.Dto;
using Interface.Models;

namespace Logic.Classes;

public class Skill(SkillDto skillDto)
{
    public int Id { get; set; } = skillDto.Id;
    public int CategoryId { get; set; } = skillDto.CategoryId;
    public string Name { get; set; } = skillDto.Name;

    public SkillDto ToDto()
    {
        return new SkillDto
        {
            Id = Id,
            CategoryId = CategoryId,
            Name = Name,
        };
    }
}