using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.AssignmentSubdomain.Candidate;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;

namespace Service.Modules.AssignmentModule
{
    public interface ICandidateService
    {
        Assignment GetAssignmentByID(int IDAssignment);
        Submission GetLastSubmission(int IDAssignment, Guid IDUser);
        bool InsertSubmission(Submission submission);
        bool UpdateSubmission(Submission submission);
    }
    public class CandidateService : ICandidateService
    {
        private readonly IAssignmentRepository assignmentRepository;
        private readonly ISubmissionRepository submissionRepository;
        private readonly UnitOfWork unitOfWork;

        public CandidateService(IAssignmentRepository assignmentRepository,
            ISubmissionRepository submissionRepository,
            UnitOfWork unitOfWork)
        {
            this.assignmentRepository = assignmentRepository;
            this.submissionRepository = submissionRepository;
            this.unitOfWork = unitOfWork;
        }

        public Assignment GetAssignmentByID(int IDAssignment)
        {
            AssignmentDTO result = assignmentRepository.GetAssignmentById(IDAssignment);
            return new Assignment()
            {
                IDAssignment = result.IDAssignment,
                IDPeriod = result.IDPeriod,
                IDStage = result.IDStage,
                IDSubStage = result.IDSubStage,
                AssignmentFileName = result.FilePath,
                Notes = result.Notes,
                DeadlineStartDate = result.DeadlineStart,
                DeadlineEndDate = result.DeadlineEnd,
            };
        }

        public Submission GetLastSubmission(int IDAssignment, Guid IDUser)
        {
            SubmissionDTO submission = submissionRepository.GetLastSubmission(IDAssignment, IDUser);
            if (submission == null)
            {
                return null;
            }

            return new Submission()
            {
                IDSubmission = submission.IDSubmission,
                IDAssignment = IDAssignment,
                IDUser = IDUser,
                Notes = submission.Notes,
                FilePath = submission.FilePath,
                LastSubmit = submission.DateUp ?? submission.DateIn
            };
        }

        public bool InsertSubmission(Submission submission)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(submissionRepository).ToUse(ctx);

                    SubmissionDTO request = new SubmissionDTO()
                    {
                        IDAssignment = submission.IDAssignment,
                        IDUser = submission.IDUser,
                        Notes = submission.Notes,
                        FilePath = submission.FilePath,
                    };

                    submissionRepository.Insert(request);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSubmission(Submission submission)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(submissionRepository).ToUse(ctx);

                    SubmissionDTO request = submissionRepository.GetByID(submission.IDSubmission);                    
                    request.Notes = submission.Notes;
                    request.FilePath = submission.FilePath;                    

                    submissionRepository.Update(request);
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
