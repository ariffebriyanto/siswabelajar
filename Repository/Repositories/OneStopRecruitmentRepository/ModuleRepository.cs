using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IModuleRepository : IRepository<ModuleDTO>
    {
        ModuleDTO GetModuleById(Guid IDModule);
        IEnumerable<ModuleDTO> GetModuleByIdRole(int IDRole);
        IEnumerable<ModuleDTO> GetModuleByURL(string URL);
    }

    public class ModuleRepository : BaseRepository<ModuleDTO>, IModuleRepository
    {
        public ModuleRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public ModuleDTO GetModuleById(Guid IDModule)
        {
            return Context.moduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDModule.Equals(IDModule)
                ).FirstOrDefault();
        }

        public IEnumerable<ModuleDTO> GetModuleByIdRole(int IDRole)
        {
            return Context.moduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDRole == IDRole
                );
        }

        public IEnumerable<ModuleDTO> GetModuleByURL(string URL)
        {
            return Context.moduleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            x.Route.Contains(URL)
                );
        }
    }
}