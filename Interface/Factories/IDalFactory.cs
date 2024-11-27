using Interface.Interface.Dal;

namespace Interface.Factories;

public interface IDalFactory
{
    public ICategoryDal GetCategoryDal();
    public IFlexworkerDal GetFlexWorkerDal();
    public IJobDal GetJobDal();
    public ISkillDal GetSkillDal();
    
}