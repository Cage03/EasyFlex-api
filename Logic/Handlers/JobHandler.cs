using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers;

public class JobHandler(IJobDal jobDal) : IJobHandler
{
    public async Task CreateJob(JobModel job)
    {
        await jobDal.CreateJob(job);
    }

    public async Task<JobModel?> GetJob(int id)
    {
        return await jobDal.GetJob(id);
    }

    public async Task UpdateJob(JobModel job)
    {
        await jobDal.UpdateJob(job);
    }

    public async Task DeleteJob(JobModel job)
    {
        await jobDal.DeleteJob(job);
    }

    public async Task<JobModel[]?> GetJobs(int pageNumber)
    {
        const int limit = 6;
        var offset = (pageNumber - 1) * limit; 
        
        var jobs = await jobDal.GetJobs()
            .Skip(offset) 
            .Take(limit)  
            .ToArrayAsync();

        return jobs;
    }
}