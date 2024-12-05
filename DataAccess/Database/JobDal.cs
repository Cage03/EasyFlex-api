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

    public async Task<List<JobModel>> GetJobs(int offset, int limit)
    {
        return await context.Jobs
            .Skip(offset)
            .Take(limit)
            .ToListAsync(); 
    }

    public async Task<JobModel> GetJob(int id)
    {
        var job = await context.Jobs
            .Include(j => j.Preferences)
            .FirstOrDefaultAsync(j => j.Id == id);

        if (job == null)
        {
            throw new NotFoundException("Job not found");
        }

        return job;
    }

    public async Task UpdateJob(JobModel job)
    {
        var originalJob = await context.Jobs
            .Include(j => j.Preferences)
            .FirstOrDefaultAsync(j => j.Id == job.Id);

        if (originalJob == null)
        {
            throw new NotFoundException($"Job with ID {job.Id} not found.");
        }

        //The below functions are necessary to differentiate between updating, adding or removing a preference
        UpdateScalarProperties(originalJob, job);
        SynchronizePreferences(originalJob, job.Preferences);
        
        await context.SaveChangesAsync();
    }

    private void UpdateScalarProperties(JobModel originalJob, JobModel updatedJob)
    {
        foreach (var property in typeof(JobModel).GetProperties())
        {
            if (property.CanWrite && property.Name != nameof(JobModel.Preferences))
            {
                var newValue = property.GetValue(updatedJob);
                property.SetValue(originalJob, newValue);
            }
        }
    }

    private void SynchronizePreferences(JobModel originalJob, ICollection<PreferenceModel> updatedPreferences)
    {
        foreach (var updatedPreference in updatedPreferences)
        {
            var existingPreference = originalJob.Preferences
                .FirstOrDefault(p => p.Id == updatedPreference.Id);

            if (existingPreference != null)
            {
                UpdatePreference(existingPreference, updatedPreference);
            }
            else
            {
                if (context.Entry(updatedPreference).State == EntityState.Detached)
                {
                    context.Preferences.Attach(updatedPreference);
                }
                originalJob.Preferences.Add(updatedPreference);
            }
        }
        
        var preferencesToRemove = originalJob.Preferences
            .Where(p => updatedPreferences.All(updated => updated.Id != p.Id))
            .ToList();

        foreach (var preference in preferencesToRemove)
        {
            originalJob.Preferences.Remove(preference);
            context.Preferences.Remove(preference); 
        }
    }

    private void UpdatePreference(PreferenceModel existingPreference, PreferenceModel updatedPreference)
    {
        foreach (var property in typeof(PreferenceModel).GetProperties())
        {
            if (property.CanWrite)
            {
                var newValue = property.GetValue(updatedPreference);
                property.SetValue(existingPreference, newValue);
            }
        }
    }

    public async Task DeleteJob(int id)
    {
        var job = await GetJob(id);
        context.Jobs.Remove(job);
        await context.SaveChangesAsync();
    }
}