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

    public async Task<List<JobModel>> GetJobsBySkillIds(List<int> skillIds)
    {
        return await context.Jobs
            .Include(j => j.Preferences)
            .Where(j => j.Preferences.Any(p => skillIds.Contains(p.SkillId)))
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

        if (originalJob != null)
        {
            // Update parent
            context.Entry(originalJob).CurrentValues.SetValues(job);

            // Delete children
            foreach (var existingPreference in originalJob.Preferences.ToList())
            {
                if (!job.Preferences.Any(c => c.Id == existingPreference.Id))
                    context.Preferences.Remove(existingPreference);
            }

            // Update and Insert children
            foreach (var newPreference in job.Preferences)
            {
                var existingChild = originalJob.Preferences
                    .SingleOrDefault(c => c.Id == newPreference.Id && c.Id != default(int));

                if (existingChild != null)
                    // Update child
                    context.Entry(existingChild).CurrentValues.SetValues(newPreference);
                else
                {
                    // Insert child
                    var newAddedPreference = new PreferenceModel()
                    {
                        SkillId = newPreference.SkillId,
                        JobId = job.Id,
                        IsRequired = newPreference.IsRequired,
                        Weight = newPreference.Weight,
                        Job = originalJob,
                        Skill = context.Skills.FirstOrDefault(x => x.Id == newPreference.SkillId),
                    };
                    originalJob.Preferences.Add(newAddedPreference);
                }
            }

        }
        else 
        {
            throw new NotFoundException($"Job with ID {job.Id} not found.");
        }

        await context.SaveChangesAsync();
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