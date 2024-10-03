using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;

namespace DataAccess.Database;

public class JobDal(dbo context) : IJobDal
{
    public async Task CreateJob(JobModel job)
    {
        context.Jobs.Add(job);
        await context.SaveChangesAsync();
    }

    public async Task DeleteJob(JobModel job)
    {
        context.Jobs.Remove(job);
        await context.SaveChangesAsync();
    }

    public Task GetAllJobs()
    {
        throw new NotImplementedException();
    }

    public async Task<JobModel?> GetJob(int id)
    {
        return await context.Jobs.FindAsync(id);
    }

    public async Task UpdateJob(JobModel job)
    {
        context.Jobs.Update(job);
        await context.SaveChangesAsync();
    }
}