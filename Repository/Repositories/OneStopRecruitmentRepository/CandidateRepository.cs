using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.EmailSubdomain;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface ICandidateRepository : IRepository<CandidateDTO>
    {
        IEnumerable<CandidateDTO> GetCandidateByStage(int IDPeriod, int IDStage, string IDPosition);
        IEnumerable<CandidateDTO> GetCandidateByCandidateIdList(List<Guid> CandidateIdList);
        CandidateDTO GetCandidateByNIM(string NIM);
        IEnumerable<CandidateDTO> GetCandidateByPeriod(int IDPeriod);
        IEnumerable<CandidateDTO> GetCandidateByNIMList(List<string> NIMList);
        List<BlastEmail> GetActiveCandidatesEmail();
    }
    public class CandidateRepository : BaseRepository<CandidateDTO>, ICandidateRepository
    {
        public CandidateRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public IEnumerable<CandidateDTO> GetCandidateByStage(int IDPeriod, int IDStage, string IDPosition)
        {
            return Context.candidateDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod && 
                            x.IDPosition.Equals(IDPosition) &&
                            x.IDStage >= IDStage);
        }

        public IEnumerable<CandidateDTO> GetCandidateByCandidateIdList(List<Guid> CandidateIdList)
        {
            return Context.candidateDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            CandidateIdList.Contains(x.IDCandidate));
        }

        public CandidateDTO GetCandidateByNIM(string NIM)
        {
            return Context.candidateDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.NIM.Equals(NIM))
                .OrderByDescending(x => x.IDPeriod)
                .FirstOrDefault();
        }

        public IEnumerable<CandidateDTO> GetCandidateByPeriod(int IDPeriod)
        {
            return Context.candidateDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDPeriod == IDPeriod);
        }

        public IEnumerable<CandidateDTO> GetCandidateByNIMList(List<string> NIMList)
        {
            return Context.candidateDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            NIMList.Contains(x.NIM));
        }

        public List<BlastEmail> GetActiveCandidatesEmail()
        {
            return (
                from C in this.Context.candidateDTOs
                join P in this.Context.periodDTOs on new { C.IDPeriod, C.IDStage } equals new { P.IDPeriod, P.IDStage }
                where !C.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !P.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where P.IsActive == 1
                select new BlastEmail
                {
                    IDPeriod = P.IDPeriod,
                    RecipientEmail = C.Email
                }
            ).Distinct().ToList();
        }
    }
}
