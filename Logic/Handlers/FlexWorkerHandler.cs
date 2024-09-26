using Interface.Interface.Dal;
using Interface.Interface.Handlers;

namespace Logic.Handlers;

public class FlexWorkerHandler(IFlexWorkerDal flexWorkerDal) : IFlexWorkerHandler
{
    public Task CreateFlexWorker()
    {
        throw new NotImplementedException();
    }
}