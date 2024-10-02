using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class JobHandler(IJobDal jobDal) : IJobHandler
{
    public async Task CreateJob(JobModel job)
    {
        await jobDal.CreateJob(job);
    }

    public async Task UpdateJob(JobModel job)
    {
        await jobDal.UpdateJob(job);
    }
}