using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace Logic.Handlers;

public class JobHandler(IJobDal jobDal) : IJobHandler
{
    public async Task<int> CreateJob(JobModel job)
    {
        return await jobDal.CreateJob(job);
    }

    public async Task<JobModel?> GetJob(int id)
    {
        return await jobDal.GetJob(id);
    }

    public async Task UpdateJob(JobModel job)
    {
        await jobDal.UpdateJob(job);
    }

    public async Task DeleteJob(int id)
    {
        if (id <= 0) throw new IndexOutOfRangeException();
        
        await jobDal.DeleteJob(id);
    }

    public async Task<JobModel[]?> GetJobs(int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        
        var jobs = await jobDal.GetJobs(offset, limit);
        
        return jobs?.ToArray();
    }
}