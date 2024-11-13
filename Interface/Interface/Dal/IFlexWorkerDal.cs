using Interface.Models;

namespace Interface.Interface.Dal;

public interface IFlexWorkerDal
{
    public Task<List<FlexworkerModel>> GetFlexWorkersByPage(int limit, int page);
    public Task<List<FlexworkerModel>> GetAllFlexWorkers();
    public Task AddFlexWorker(FlexworkerModel flexWorker);
    public Task<FlexworkerModel?> GetFlexWorkerById(int id);
    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
    public Task DeleteFlexWorker(int id);
}