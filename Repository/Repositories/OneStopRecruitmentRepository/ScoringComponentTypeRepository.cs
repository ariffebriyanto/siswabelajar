using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IScoringComponentTypeRepository : IRepository<ScoringComponentTypeDTO>
    {
        ScoringComponentTypeDTO SearchScoringComponentType(string ScoringComponentType);
        IEnumerable<ScoringComponentTypeDTO> GetAllScoringComponentType();
        IEnumerable<ScoringComponentTypeDTO> GetScoringComponentTypeByListID(List<Guid> IDScoringComponentTypeList);
    }
    public class ScoringComponentTypeRepository : BaseRepository<ScoringComponentTypeDTO>, IScoringComponentTypeRepository
    {
        public ScoringComponentTypeRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public ScoringComponentTypeDTO SearchScoringComponentType(string ScoringComponentType)
        {
            return Context.scoringComponentTypeDTOs.Where(x => x.ScoringComponentType.Trim().ToLower().Equals(ScoringComponentType.Trim().ToLower())
                && !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && !x.StsRc.Equals(BaseConstraint.StsRc.Delete)
                ).FirstOrDefault();
        }

        public IEnumerable<ScoringComponentTypeDTO> GetAllScoringComponentType()
        {
            return Context.scoringComponentTypeDTOs.Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) && 
            !x.StsRc.Equals(BaseConstraint.StsRc.Delete));
        }

        public IEnumerable<ScoringComponentTypeDTO> GetScoringComponentTypeByListID(List<Guid> IDScoringComponentTypeList)
        {
            return Context.scoringComponentTypeDTOs.Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                !x.StsRc.Equals(BaseConstraint.StsRc.Delete) && IDScoringComponentTypeList.Contains(x.IDScoringComponentType));
        }
    }
}
