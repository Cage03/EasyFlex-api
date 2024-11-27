using Interface.Dtos;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;


namespace Logic.Handlers;

public class FlexWorkerHandler(IFlexworkerDal flexworkerDal) : IFlexworkerHandler
{
    public async Task CreateFlexworker(Flexworker flexworker)
    { 
        await flexworkerDal.AddFlexWorker(ToModel(flexworker));
    }

    public async Task<List<Flexworker>> GetFlexworkers(int limit, int page)
    {
        List<Flexworker> list = new List<Flexworker>();
        foreach (FlexworkerModel model in await flexworkerDal.GetAllFlexworkers(limit, page))
        {
            list.Add(ToDto(model));
        }
        return list;
    }

    public async Task<Flexworker> GetFlexworkerById(int id)
    {
        FlexworkerModel model = await flexworkerDal.GetFlexworkerById(id);

        return ToDto(model);
    }

    public async Task UpdateFlexworker(Flexworker flexworker)
    {
        await flexworkerDal.UpdateFlexWorker(ToModel(flexworker));
    }

    public async Task DeleteFlexworker(int id)
    {
        await flexworkerDal.DeleteFlexworker(id);
    }

    public async Task AddSkills(int flexworkerId, List<Skill> skills)
    {
        if (skills.Count == 0)
        {
            throw new Exception("No skills provided");
        }
        List<SkillModel> skillModels = skills.Select(SkillHandler.ToModel).ToList();
        await flexworkerDal.AddSkills(flexworkerId, skillModels);
    }

    public async Task RemoveSkills(int flexworkerId, List<Skill> skills)
    {
        if (skills.Count == 0)
        {
            throw new Exception("No skills provided");
        }
        List<SkillModel> skillModels = skills.Select(SkillHandler.ToModel).ToList();
        await flexworkerDal.RemoveSkills(flexworkerId, skillModels);
    }

    public static FlexworkerModel ToModel(Flexworker flexworker)
    {
        return new FlexworkerModel
        {
            Id = flexworker.Id,
            Name = flexworker.Name,
            Address = flexworker.Address,
            DateOfBirth = flexworker.DateOfBirth,
            Email = flexworker.Email,
            PhoneNumber = flexworker.PhoneNumber,
            ProfilePictureUrl = flexworker.ProfilePictureUrl,
            Skills = flexworker.Skills.Select(s => SkillHandler.ToModel(s)).ToList()
        };
    }

    public static Flexworker ToDto(FlexworkerModel flexworkerModel)
    {
        return new Flexworker()
        {
            Id = flexworkerModel.Id,
            Name = flexworkerModel.Name,
            Address = flexworkerModel.Address,
            DateOfBirth = flexworkerModel.DateOfBirth,
            Email = flexworkerModel.Email,
            PhoneNumber = flexworkerModel.PhoneNumber,
            ProfilePictureUrl = flexworkerModel.ProfilePictureUrl,
            Skills = flexworkerModel.Skills.Select(s => SkillHandler.ToDto(s)).ToList()
        };
    }
}