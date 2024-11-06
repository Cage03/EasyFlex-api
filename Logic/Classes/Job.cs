using Interface.Models;

namespace Logic.Classes;

public class Job
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string? Description { get; set; }
    public int MinHours { get; set; }
    public int MaxHours { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public virtual ICollection<Skill> Skills { get; set; }

    public Job(JobModel jobModel)
    {
        Id = jobModel.Id;
        Address = jobModel.Address;
        Description = jobModel.Description;
        MinHours = jobModel.MinHours;
        MaxHours = jobModel.MaxHours;
        StartDate = jobModel.StartDate;
        EndDate = jobModel.EndDate;
        Skills = jobModel.Skills.Select(s => new Skill(s)).ToList();
    }
    public JobModel ToModel()
    {
        return new JobModel
        {
            Id = Id,
            Address = Address,
            Description = Description,
            MinHours = MinHours,
            MaxHours = MaxHours,
            StartDate = StartDate,
            EndDate = EndDate,
            Skills = Skills.Select(s => s.ToModel()).ToList()
        };
    }
}