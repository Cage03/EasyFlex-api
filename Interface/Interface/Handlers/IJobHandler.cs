using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IJobHandler
{
    public Task CreateJob(JobModel job);
}