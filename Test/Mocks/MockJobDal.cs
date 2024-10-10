using Interface.Interface.Dal;
using Interface.Models;

namespace Test.Mocks;

public class MockJobDal : IJobDal
{
    private readonly List<JobModel> _jobs;

    public MockJobDal(List<JobModel> initialJobs)
    {
        _jobs = initialJobs;
    }

    public Task CreateJob(JobModel job)
    {
        _jobs.Add(job);
        return Task.CompletedTask;
    }

    public Task DeleteJob(JobModel job)
    {
        _jobs.Remove(job);
        return Task.CompletedTask;
    }

    public IQueryable<JobModel> GetJobs()
    {
        return _jobs.AsQueryable();
    }

    public Task<JobModel?> GetJob(int id)
    {
        var job = _jobs.FirstOrDefault(j => j.Id == id);
        return Task.FromResult(job);
    }

    public Task UpdateJob(JobModel job)
    {
        var existingJob = _jobs.FirstOrDefault(j => j.Id == job.Id);
        if (existingJob != null)
        {
            existingJob.Name = job.Name;
            existingJob.Address = job.Address;
            existingJob.Description = job.Description;
            existingJob.MinHours = job.MinHours;
            existingJob.MaxHours = job.MaxHours;
            existingJob.StartDate = job.StartDate;
            existingJob.EndDate = job.EndDate;
        }
        return Task.CompletedTask;
    }
}