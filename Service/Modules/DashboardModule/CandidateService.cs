using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.DashboardSubdomain.Candidate;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.DashboardModule
{
    public interface ICandidateService
    {
        string GetCurrentPeriod(string NIM);
        string GetCurrentStage(string NIM);
        MasterSchedule GetScheduleByID(Guid IDSchedule);
        List<MasterSchedule> GetAvailableSchedules(string NIM, Guid IDUser);
        List<MasterSchedule> GetCandidateSchedules(Guid IDUser);
        List<Assignment> GetAvailableAssignments(string NIM);
        List<Assignment> GetUnsubmittedAssignments(string NIM, Guid IDUser);
        List<BlastEmail> GetNotifications();
        bool CancelCandidateSchedule(Guid IDSchedule, Guid IDUser);
        bool EnrollSchedule(Guid IDSchedule, Guid IDUser);
        List<MasterSchedule> GetUnenrolledSchedules(string NIM, Guid IDUser);
    }
    public class CandidateService : ICandidateService
    {
        private readonly IMasterScheduleRepository masterScheduleRepository;
        private readonly ITransactionScheduleRepository transactionScheduleRepository;
        private readonly IStageRepository stageRepository;
        private readonly ISubStageRepository subStageRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserRepository userRepository;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IBlastEmailRepository blastEmailRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly UnitOfWork unitOfWork;

        public CandidateService(IMasterScheduleRepository masterScheduleRepository,
            ITransactionScheduleRepository transactionScheduleRepository,
            IStageRepository stageRepository,
            ISubStageRepository subStageRepository,
            IPeriodRepository periodRepository,
            IPositionRepository positionRepository,
            ICandidateRepository candidateRepository,
            IUserRepository userRepository,
            IAssignmentRepository assignmentRepository,
            IBlastEmailRepository blastEmailRepository,
            ISubmissionRepository submissionRepository,
            UnitOfWork unitOfWork)
        {
            this.masterScheduleRepository = masterScheduleRepository;
            this.transactionScheduleRepository = transactionScheduleRepository;
            this.stageRepository = stageRepository;
            this.subStageRepository = subStageRepository;
            this.periodRepository = periodRepository;
            this.positionRepository = positionRepository;
            this.candidateRepository = candidateRepository;
            this.userRepository = userRepository;
            this.assignmentRepository = assignmentRepository;
            this.blastEmailRepository = blastEmailRepository;
            this.submissionRepository = submissionRepository;
            this.unitOfWork = unitOfWork;
        }

        public string GetCurrentPeriod(string NIM)
        {
            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);
            
            if(candidate != null)
            {
                PeriodDTO period = periodRepository.GetPeriodByID(candidate.IDPeriod);
                return period != null ? period.PeriodName : "-";
            }
            return "-";
        }

        public string GetCurrentStage(string NIM)
        {
            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);

            if (candidate != null)
            {
                StageDTO stage = stageRepository.GetStageById(candidate.IDStage);
                return stage != null ? stage.StageName : "-";
            }
            return "-";
        }

        public MasterSchedule GetScheduleByID(Guid IDSchedule)
        {
            MasterScheduleDTO scheduleDTO = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDSchedule(IDSchedule).ToList();

            return new MasterSchedule()
            {
                IDSchedule = IDSchedule,
                IDPeriod = scheduleDTO.IDPeriod,
                IDPosition = scheduleDTO.IDPosition,
                IDStage = scheduleDTO.IDStage,
                IDSubStage = scheduleDTO.IDSubStage,
                Date = scheduleDTO.Date,
                StartTime = scheduleDTO.StartTime,
                EndTime = scheduleDTO.EndTime,
                Limit = scheduleDTO.Limit,
                Room = scheduleDTO.Room,
                Qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Count : 0
            };
        }

        public List<MasterSchedule> GetAvailableSchedules(string NIM, Guid IDUser)
        {
            List<MasterSchedule> result = new List<MasterSchedule>();
            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();
            PeriodDTO period = periodRepository.GetActivePeriod();
            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);
            
            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period.IDPeriod)
                .Where(x => (x.Date.Date.CompareTo(DateTime.Now.Date) > 0 || x.Date.Date.CompareTo(DateTime.Now.Date) == 0 && x.StartTime.CompareTo(DateTime.Now.TimeOfDay) > 0)
                            && x.IDPosition.Contains(candidate.IDPosition))
                .OrderBy(x => x.Date).ToList();

            List<Guid> IDScheduleCandidate = GetCandidateSchedules(IDUser).Select(x => x.IDSchedule).ToList();
            List<Guid> IDScheduleList = scheduleDTOs.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();

            foreach (MasterScheduleDTO item in scheduleDTOs)
            {
                if (!IDScheduleCandidate.Contains(item.IDSchedule))
                {
                    StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                    SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                    int qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).Count() : 0;

                    MasterSchedule schedule = new MasterSchedule()
                    {
                        IDSchedule = item.IDSchedule,
                        IDPeriod = item.IDPeriod,
                        IDPosition = item.IDPosition,
                        IDStage = item.IDStage,
                        IDSubStage = item.IDSubStage,
                        Date = item.Date,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        Limit = item.Limit,
                        Room = item.Room,
                        Qty = qty,
                        StageName = stage != null ? stage.StageName : "",
                        SubStageName = subStage != null ? subStage.SubStageName : "",
                    };

                    result.Add(schedule);                                 
                }
            }

            result = result.OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
                .ThenBy(x => x.IDStage)
                .ThenBy(x => x.IDSubStage)
                .ToList();

            return result;
        }

        public List<MasterSchedule> GetUnenrolledSchedules(string NIM, Guid IDUser)
        {
            List<MasterSchedule> result = new List<MasterSchedule>();
            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();
            PeriodDTO period = periodRepository.GetActivePeriod();
            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);

            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period.IDPeriod)
                .Where(x => (x.Date.Date.CompareTo(DateTime.Now.Date) > 0 || x.Date.Date.CompareTo(DateTime.Now.Date) == 0 && x.StartTime.CompareTo(DateTime.Now.TimeOfDay) > 0)
                            && x.IDPosition.Contains(candidate.IDPosition))
                .OrderByDescending(x => x.Date).ThenBy(x=>x.StartTime).ToList();

            List<Guid> IDScheduleCandidate = GetCandidateSchedules(IDUser).Select(x => x.IDSchedule).ToList();
            List<Guid> IDScheduleList = scheduleDTOs.Select(x => x.IDSchedule).ToList();
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDScheduleList).ToList();

            foreach (MasterScheduleDTO item in scheduleDTOs)
            {
                if (!IDScheduleCandidate.Contains(item.IDSchedule))
                {
                    // Search for the same schedule
                    List<Guid> CandidateIDScheduleList = transactionScheduleRepository.GetByIDUser(IDUser)
                                                .Select(x => x.IDSchedule).ToList();
                    MasterScheduleDTO enrollSchedule = masterScheduleRepository.GetScheduleByIDSchedule(item.IDSchedule);
                    List<MasterScheduleDTO> scheduleList = masterScheduleRepository.GetScheduleByListIDSchedule(CandidateIDScheduleList).ToList();
                    MasterScheduleDTO search = scheduleList.Where(x => x.IDPeriod == enrollSchedule.IDPeriod &&
                                            x.IDStage == enrollSchedule.IDStage &&
                                            x.IDSubStage == enrollSchedule.IDSubStage).FirstOrDefault();
                    int flag = 0;

                    if (search == null)
                    {
                        foreach (MasterSchedule ms in result)
                        {
                            if (ms.IDPeriod == enrollSchedule.IDPeriod && ms.IDStage == enrollSchedule.IDStage && ms.IDSubStage == enrollSchedule.IDSubStage)
                            {
                                flag = 1; 
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                            SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();
                            int qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).Count() : 0;

                            MasterSchedule schedule = new MasterSchedule()
                            {
                                IDSchedule = item.IDSchedule,
                                IDPeriod = item.IDPeriod,
                                IDPosition = item.IDPosition,
                                IDStage = item.IDStage,
                                IDSubStage = item.IDSubStage,
                                Date = item.Date,
                                StartTime = item.StartTime,
                                EndTime = item.EndTime,
                                Limit = item.Limit,
                                Room = item.Room,
                                Qty = qty,
                                StageName = stage != null ? stage.StageName : "",
                                SubStageName = subStage != null ? subStage.SubStageName : "",
                            };

                            result.Add(schedule);
                        }
                    }
                }
            }

            result = result.OrderBy(x => x.Date)
                .ThenBy(x => x.StartTime)
                .ThenBy(x => x.IDStage)
                .ThenBy(x => x.IDSubStage)
                .ToList();

            return result;
        }


        public List<MasterSchedule> GetCandidateSchedules(Guid IDUser)
        {
            List<MasterSchedule> result = new List<MasterSchedule>();
            List<TransactionScheduleDTO> transactionCandidateScheduleDTOs = transactionScheduleRepository.GetByIDUser(IDUser).ToList();
            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();
            
            List<Guid> IDCandidateScheduleList = transactionCandidateScheduleDTOs.Select(x => x.IDSchedule).Distinct().ToList();
            List<MasterScheduleDTO> candidateScheduleDTOs = masterScheduleRepository.GetScheduleByListIDSchedule(IDCandidateScheduleList).ToList();

            // For Count Candidate Qty
            List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDScheduleList(IDCandidateScheduleList).ToList();

            foreach (var item in candidateScheduleDTOs)
            {
                StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();

                int qty = transactionScheduleDTOs != null ? transactionScheduleDTOs.Where(x => x.IDSchedule.Equals(item.IDSchedule)).Count() : 0;
                MasterSchedule schedule = new MasterSchedule()
                {
                    IDSchedule = item.IDSchedule,
                    IDPeriod = item.IDPeriod,
                    IDPosition = item.IDPosition,
                    IDStage = item.IDStage,
                    IDSubStage = item.IDSubStage,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Limit = item.Limit,
                    Room = item.Room,
                    Qty = qty,
                    StageName = stage != null ? stage.StageName : "",
                    SubStageName = subStage != null ? subStage.SubStageName : "",
                };

                result.Add(schedule);
            }

            result = result.OrderBy(x => x.Date).ToList();

            return result;
        }

        public List<Assignment> GetAvailableAssignments(string NIM)
        {
            List<Assignment> result = new List<Assignment>();

            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();

            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);

            PeriodDTO period = periodRepository.GetActivePeriod();            
            List<AssignmentDTO> assignmentDTOs = assignmentRepository.GetAssignmentByIdPeriod(period != null ? period.IDPeriod : 0).ToList();
            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period != null ? period.IDPeriod : 0).ToList();

            foreach (var item in assignmentDTOs)
            {
                MasterScheduleDTO schedule = scheduleDTOs.Where(x => x.IDStage == item.IDStage && 
                                                                    x.IDSubStage == item.IDSubStage &&
                                                                    x.IDPosition.Contains(candidate.IDPosition)).FirstOrDefault();
                if(schedule != null)
                {
                    StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                    SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();

                    Assignment assignment = new Assignment()
                    {
                        IDAssignment = item.IDAssignment,
                        IDPeriod = item.IDPeriod,
                        IDStage = item.IDStage,
                        IDSubStage = item.IDSubStage,
                        DeadlineStartDate = item.DeadlineStart,
                        DeadlineEndDate = item.DeadlineEnd,
                        StageName = stage != null ? stage.StageName : "",
                        SubStageName = subStage != null ? subStage.SubStageName : ""
                    };

                    result.Add(assignment);
                }               
            }

            result = result.OrderBy(x => x.DeadlineStartDate).ThenBy(x => x.DeadlineEndDate).ToList();

            return result;
        }

        public List<Assignment> GetUnsubmittedAssignments(string NIM, Guid IDUser)
        {
            List<Assignment> result = new List<Assignment>();

            List<StageDTO> stageDTOs = stageRepository.FindAll().ToList();
            List<SubStageDTO> subStageDTOs = subStageRepository.FindAll().ToList();

            CandidateDTO candidate = candidateRepository.GetCandidateByNIM(NIM);

            PeriodDTO period = periodRepository.GetActivePeriod();
            List<AssignmentDTO> assignmentDTOs = assignmentRepository.GetAssignmentByIdPeriod(period != null ? period.IDPeriod : 0).ToList();
            List<MasterScheduleDTO> scheduleDTOs = masterScheduleRepository.GetScheduleByPeriod(period != null ? period.IDPeriod : 0).ToList();

            foreach (var item in assignmentDTOs)
            {
                if (submissionRepository.GetLastSubmission(item.IDAssignment, IDUser) == null)
                {
                    MasterScheduleDTO schedule = scheduleDTOs.Where(x => x.IDStage == item.IDStage &&
                                                                    x.IDSubStage == item.IDSubStage &&
                                                                    x.IDPosition.Contains(candidate.IDPosition)).FirstOrDefault();
                    if (schedule != null)
                    {
                        StageDTO stage = stageDTOs.Where(x => x.IDStage == item.IDStage).FirstOrDefault();
                        SubStageDTO subStage = subStageDTOs.Where(x => x.IDSubStage == item.IDSubStage).FirstOrDefault();

                        Assignment assignment = new Assignment()
                        {
                            IDAssignment = item.IDAssignment,
                            IDPeriod = item.IDPeriod,
                            IDStage = item.IDStage,
                            IDSubStage = item.IDSubStage,
                            DeadlineStartDate = item.DeadlineStart,
                            DeadlineEndDate = item.DeadlineEnd,
                            StageName = stage != null ? stage.StageName : "",
                            SubStageName = subStage != null ? subStage.SubStageName : ""
                        };

                        result.Add(assignment);
                    }
                }
            }

            result = result.OrderBy(x => x.DeadlineStartDate).ThenBy(x => x.DeadlineEndDate).ToList();

            return result;
        }

        public List<BlastEmail> GetNotifications()
        {
            List<BlastEmail> result = new List<BlastEmail>();

            PeriodDTO period = periodRepository.GetActivePeriod();
            List<BlastEmailDTO> blastEmailDTOs = blastEmailRepository.GetByIDPeriod(period != null ? period.IDPeriod : 0).ToList();
            
            foreach(var item in blastEmailDTOs)
            {
                BlastEmail blastEmail = new BlastEmail()
                {
                    IDBlastEmail = item.IDBlastEmail,
                    IDPeriod = item.IDPeriod,
                    Subject = item.Subject,
                    Description = item.Description,
                    BlastDateTime = item.BlastDateTime
                };
                result.Add(blastEmail);
            }

            result = result.OrderByDescending(x => x.BlastDateTime).ToList();

            return result;
        }

        public bool CancelCandidateSchedule(Guid IDSchedule, Guid IDUser)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(transactionScheduleRepository).ToUse(ctx);

                    TransactionScheduleDTO request = transactionScheduleRepository.GetByScheduleAndUser(IDSchedule, IDUser);
                    if (request != null)
                    {
                        transactionScheduleRepository.Delete(request);
                    }
                });                  
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EnrollSchedule(Guid IDSchedule, Guid IDUser)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(transactionScheduleRepository).ToUse(ctx);

                    // Search for the same schedule
                    List<Guid> IDScheduleList = transactionScheduleRepository.GetByIDUser(IDUser)
                                                .Select(x => x.IDSchedule).ToList();
                    MasterScheduleDTO enrollSchedule = masterScheduleRepository.GetScheduleByIDSchedule(IDSchedule);
                    List<MasterScheduleDTO> scheduleList = masterScheduleRepository.GetScheduleByListIDSchedule(IDScheduleList).ToList();
                    MasterScheduleDTO search = scheduleList.Where(x => x.IDPeriod == enrollSchedule.IDPeriod &&
                                            x.IDStage == enrollSchedule.IDStage &&
                                            x.IDSubStage == enrollSchedule.IDSubStage).FirstOrDefault();
                    
                    List<TransactionScheduleDTO> transactionScheduleDTOs = transactionScheduleRepository.GetByIDSchedule(IDSchedule).ToList();
                    int qty = transactionScheduleDTOs == null ? 0 : transactionScheduleDTOs.Count();

                    if (search != null)
                    {
                        // Delete
                        TransactionScheduleDTO deleteTransaction = transactionScheduleRepository.GetByScheduleAndUser(search.IDSchedule, IDUser);
                        if (deleteTransaction != null)
                        {
                            transactionScheduleRepository.Delete(deleteTransaction);
                        }
                    }

                    if(qty < enrollSchedule.Limit)
                    {
                        TransactionScheduleDTO request = new TransactionScheduleDTO()
                        {
                            IDSchedule = IDSchedule,
                            IDUser = IDUser
                        };
                        transactionScheduleRepository.Insert(request);
                    }
                    else
                    {
                        throw new Exception("Room is Full!");
                    }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
