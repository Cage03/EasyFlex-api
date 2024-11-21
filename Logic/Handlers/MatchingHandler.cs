using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class MatchingHandler(IFlexWorkerDal flexWorkerDal, IJobDal jobDal) : IMatchingHandler
{
    public async Task<List<FlexworkerResultModel>> GetMatches(int jobId)
    {
        var job = await jobDal.GetJob(jobId);
        
        if (job == null) throw new NullReferenceException();
        
        var flexWorkers = await flexWorkerDal.GetAllFlexWorkers();
        
        return Algorithm.FindFlexworkersForJob(job, flexWorkers);
    }
}