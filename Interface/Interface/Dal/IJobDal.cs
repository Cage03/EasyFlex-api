using Interface.Models;

namespace Interface.Interface.Dal;

public interface IJobDal
{
    public Task CreateJob(JobModel job);
    public Task<JobModel?> GetJob(int id);
    public IQueryable<JobModel> GetJobs();
    public Task UpdateJob(JobModel job);
    public Task DeleteJob(JobModel job);
}