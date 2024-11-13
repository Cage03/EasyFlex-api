using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IMatchingHandler
{
    public Task<List<ResultModel>> GetMatches(int jobId);
}