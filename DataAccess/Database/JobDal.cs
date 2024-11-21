using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class JobDal(EasyFlexContext context) : IJobDal
{
    public async Task<int> CreateJob(JobModel job)
    {
        context.Jobs.Add(job);
        await context.SaveChangesAsync();
        int id = job.Id; 
        return id;
    }

    public async Task DeleteJob(int id)
    {
        var job = await context.Jobs.FindAsync(id);
        
        if (job != null) context.Jobs.Remove(job);
        await context.SaveChangesAsync();
    }

    public async Task<List<JobModel>> GetJobs(int offset, int limit)
    {
        return await context.Jobs
            .Skip(offset)
            .Take(limit)
            .ToListAsync();  // Now returns a List instead of IQueryable
    }

    public async Task<JobModel> GetJob(int id)
    {
        var job = await context.Jobs.FindAsync(id);
        if (job == null)
        {
            throw new NotFoundException("Job not found");
        }
        return job;
    }

    public async Task UpdateJob(JobModel job)
    {
        context.Jobs.Update(job);
        await context.SaveChangesAsync();
    }
}
