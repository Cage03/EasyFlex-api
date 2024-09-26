using Interface.Factories;
using Interface.Interface.Handlers;

namespace Logic.Handlers;

public class FlexWorkerHandler : IFlexWorkerHandler
{
    private readonly IDalFactory _dalFactory;

    public FlexWorkerHandler(IDalFactory dalFactory)
    {
        _dalFactory = dalFactory;
    }
    
    
}