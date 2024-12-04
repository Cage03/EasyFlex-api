namespace Interface.Models;

public class PreferenceModel
{
    public int Id { get; set; }
    public SkillModel Skill { get; set; }
    public JobModel Job { get; set; }

    public bool IsRequired { get; set; }
    public int Weight { get; set; }
}