using System.Text.Json.Serialization;

namespace Interface.Models;

public class PreferenceModel
{
    public int Id { get; set; }
    
    public int SkillId { get; set; }
    public SkillModel Skill { get; set; } = null!;
    
    public int JobId { get; set; }
    
    [JsonIgnore]
    public JobModel Job { get; set; } = null!;
    public bool IsRequired { get; set; }
    public int Weight { get; set; }
}