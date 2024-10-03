using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IJobHandler
{
    public Task CreateJob(JobModel job);
    public Task<JobModel?> GetJob(int id);
    public Task DeleteJob(JobModel job);
}