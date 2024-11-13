using Interface.Interface.Handlers;
using Interface.Interface.Dal;

namespace Interface.Factories;

public interface IHandlerFactory
{
    public IFlexWorkerHandler GetFlexWorkerHandler();
    public IJobHandler GetJobHandler();
    public ISkillHandler GetSkillHandler();
    public ICategoryHandler GetCategoryHandler();
}