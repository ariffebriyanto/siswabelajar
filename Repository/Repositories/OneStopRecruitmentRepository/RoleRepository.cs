using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IRoleRepository : IRepository<RoleDTO>
    {
        RoleDTO GetRoleById(int IDRole);
    }

    public class RoleRepository : BaseRepository<RoleDTO>, IRoleRepository
    {
        public RoleRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public RoleDTO GetRoleById(int IDRole)
        {
            return Context.roleDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDRole == IDRole
                ).FirstOrDefault();
        }
    }
}