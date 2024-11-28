using Interface.Interface.Handlers;
using Interface.Interface.Dal;

namespace Interface.Factories;

public interface IHandlerFactory
{
    public IFlexworkerHandler GetFlexworkerHandler();
    public IJobHandler GetJobHandler();
    public ISkillHandler GetSkillHandler();
    public ICategoryHandler GetCategoryHandler();
    public IMatchingHandler GetMatchingHandler();
}