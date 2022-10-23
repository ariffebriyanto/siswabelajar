using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.RegistrationSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.RegistrationModule
{
    public interface IMainService
    {
        List<Position> GetPositions();
        bool CheckCandidate(Candidate candidate);
        bool InsertCandidate(Candidate candidate);
        bool IsOpenRegistration();
    }
    public class MainService : IMainService
{
        private readonly IPositionRepository positionRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly UnitOfWork unitOfWork;

        public MainService(IPositionRepository positionRepository, ICandidateRepository candidateRepository, UnitOfWork unitOfWork, IPeriodRepository periodRepository)
        {
            this.positionRepository = positionRepository;
            this.candidateRepository = candidateRepository;
            this.periodRepository = periodRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();
            var allPosition = positionRepository.FindAll();
            foreach (var item in allPosition)
            {
                Position position = new Position();
                position.IDPosition = item.IDPosition;
                position.PositionName = item.PositionName;
                positions.Add(position);
            }
            return positions;
        }   
        
        public bool CheckCandidate(Candidate candidate)
        {
            try
            {
                PeriodDTO period = periodRepository.GetActivePeriod();

                if (period == null)
                {
                    throw new Exception("No Active Period");
                }

                CandidateDTO search = candidateRepository.FindAll().Where(x => x.NIM.Equals(candidate.NIM) && x.IDPeriod == period.IDPeriod && x.IsAccepted == 0).FirstOrDefault();
                
                return search != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertCandidate(Candidate candidate)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(candidateRepository).ToUse(ctx);
                    DateTime curr = DateTime.Now;
                    PeriodDTO period = periodRepository.GetActivePeriod();
                    if (period == null)
                    {
                        throw new Exception("No Period");
                    }
                    else if(curr.Date.CompareTo(period.DeadlineStart.Date) < 0 || curr.Date.CompareTo(period.DeadlineEnd.Date) > 0)
                    {
                        throw new Exception("Registration Closed");
                    }

                    CandidateDTO request = new CandidateDTO()
                    {
                        IDPeriod = period.IDPeriod,
                        IDStage = 1,
                        IDRole = 2,
                        IDPosition = candidate.IDPosition,
                        Email = candidate.Email,
                        NIM = candidate.NIM,
                        IsAccepted = 0
                    };

                    candidateRepository.Insert(request);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsOpenRegistration()
        {
            try
            {
                DateTime curr = DateTime.Now.Date;
                List<PeriodDTO> periodList = periodRepository.FindAll().ToList();
                foreach (var period in periodList)
                {
                    if (curr.CompareTo(period.DeadlineStart.Date) >= 0 && curr.CompareTo(period.DeadlineEnd.Date) <= 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }

            return false;
        }
    }
}