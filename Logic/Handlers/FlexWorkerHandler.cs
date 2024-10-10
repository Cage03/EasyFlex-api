using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Classes;


namespace Logic.Handlers;

public class FlexWorkerHandler(IFlexWorkerDal flexWorkerDal) : IFlexWorkerHandler
{
    public async Task CreateFlexWorker(FlexworkerModel flexWorker)
    { 
        await flexWorkerDal.AddFlexWorker(flexWorker);
    }

    public void DeleteFlexWorker(FlexworkerModel flexWorker)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FlexworkerModel>> GetFlexWorkers(int limit, int page)
    {
        return await flexWorkerDal.GetAllFlexWorkers(limit, page);
    }

    public FlexworkerModel GetFlexworkerById(int id)
    {
        return flexWorkerDal.GetFlexWorkerById(id);
    }

    public Task DeleteFlexWorker(int id)
    {
        return flexWorkerDal.DeleteFlexWorker(id);
    }

    public async Task UpdateFlexWorker(FlexworkerModel flexWorker)
    {
        await flexWorkerDal.UpdateFlexWorker(flexWorker);
    }
}