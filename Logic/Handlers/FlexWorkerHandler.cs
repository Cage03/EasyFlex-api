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

    public List<FlexworkerModel> GetFlexWorkers()
    {
        return flexWorkerDal.GetAllFlexWorkers();
    }

    public FlexworkerModel SelectFlexworkerById(int id)
    {
        throw new NotImplementedException();
    }
}