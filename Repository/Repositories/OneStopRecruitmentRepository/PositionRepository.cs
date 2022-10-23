using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IPositionRepository : IRepository<PositionDTO>
    {
        IEnumerable<PositionDTO> GetAllPosition();
        PositionDTO GetPositionById(string IDPosition);
    }

    public class PositionRepository : BaseRepository<PositionDTO>, IPositionRepository
    {
        public PositionRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<PositionDTO> GetAllPosition()
        {
            return Context.positionDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                );
        }

        public PositionDTO GetPositionById(string IDPosition)
        {
            return Context.positionDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPosition.Trim().ToUpper().Equals(IDPosition.Trim().ToUpper())
                ).FirstOrDefault();
        }
    }
}