using Interface.Dtos;
using Interface.Factories;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Logic.Factories;

namespace Logic.Handlers;

public class MatchingHandler(IFlexworkerHandler flexworkerHandler, IJobHandler jobHandler) : IMatchingHandler
{
    public async Task<List<FlexworkerResult>> GetMatchesForJob(int jobId)
    {
        Job job = await jobHandler.GetJob(jobId);
        List<int> skillIds = job.Preferences.Select(p => p.SkillId).ToList();

        List<Flexworker> flexworkers = await flexworkerHandler.GetFlexworkersBySkillIds(skillIds);

        return Algorithm.FindFlexworkersForJob(job, flexworkers);
    }
}