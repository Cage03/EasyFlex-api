using Interface.Dtos;
using Interface.Interface.Handlers;

namespace Logic.Handlers;

public class MatchingHandler(IFlexworkerHandler flexworkerHandler, IJobHandler jobHandler) : IMatchingHandler
{
    public async Task<List<FlexworkerResult>> GetMatchesForJob(int jobId)
    {
        Job job = await jobHandler.GetJob(jobId);
        List<int> skillIds = job.Preferences
            .Where(p => p.IsRequired)
            .Select(p => p.SkillId)
            .ToList();

        List<Flexworker> flexworkers = await flexworkerHandler.GetFlexworkersBySkillIds(skillIds);

        return Algorithm.FindFlexworkersForJob(job, flexworkers);
    }
}