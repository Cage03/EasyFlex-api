using Interface.Factories;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Logic.Handlers;

namespace Logic.Factories;

public class HandlerFactory(IDalFactory dalFactory) : IHandlerFactory
{
    public IFlexWorkerHandler GetFlexWorkerHandler()
    {
        return new FlexWorkerHandler(dalFactory.GetFlexWorkerDal());
    }

    public IJobHandler GetJobHandler()
    {
        return new JobHandler(dalFactory.GetJobDal());
    }

    public ISkillHandler GetSkillHandler()
    {
        return new SkillHandler(dalFactory.GetSkillDal());
    }
}