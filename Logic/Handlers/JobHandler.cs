using Interface.Dtos;
using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class JobHandler(IJobDal jobDal) : IJobHandler
{
    public async Task<int> CreateJob(Job job)
    {
        var jobModel = ToModel(job);
        return await jobDal.CreateJob(jobModel);
    }

    public async Task<Job> GetJob(int id)
    {
        var job = await jobDal.GetJob(id);
        return ToDto(job);
    }

    public async Task UpdateJob(Job job)
    {
        await jobDal.UpdateJob(ToModel(job));
    }

    public async Task DeleteJob(int id)
    {
        if (id <= 0) throw new IndexOutOfRangeException();
        
        await jobDal.DeleteJob(id);
    }

    public async Task<Job[]> GetJobs(int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        
        var jobs = await jobDal.GetJobs(offset, limit);
        return jobs.Select(ToDto).ToArray();
    }
    
    public static JobModel ToModel(Job jobDto)
    {
        return new JobModel
        {
            Id = jobDto.Id,
            Address = jobDto.Address,
            Name = jobDto.Name,
            Description = jobDto.Description,
            MinHours = jobDto.MinHours,
            MaxHours = jobDto.MaxHours,
            StartDate = jobDto.StartDate,
            EndDate = jobDto.EndDate,
            Skills = jobDto.Skills.Select(s => SkillHandler.ToModel(s)).ToList()
        };
    }
    
    public static Job ToDto(JobModel jobModel)
    {
        return new Job
        {
            Id = jobModel.Id,
            Address = jobModel.Address,
            Name = jobModel.Name,
            Description = jobModel.Description,
            MinHours = jobModel.MinHours,
            MaxHours = jobModel.MaxHours,
            StartDate = jobModel.StartDate,
            EndDate = jobModel.EndDate,
            Skills = jobModel.Skills.Select(s => SkillHandler.ToDto(s)).ToList()
        };
    }
}