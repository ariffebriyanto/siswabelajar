using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.EmailSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using Service.Helpers.EmailHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.BlastEmailModule
{
    public interface IStaffService
    {
        List<User> GetUserProfileForBlastEmail();
        bool SendBlastingEmail(string Subject, string Body);
    }

    public class StaffService : IStaffService
    {
        private readonly IUserRepository userRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IBlastEmailRepository blastEmailRepository;
        private readonly IEmailHelper emailHelper;
        private readonly UnitOfWork unitOfWork;

        public StaffService(
            IUserRepository userRepository,
            ICandidateRepository candidateRepository,
            IBlastEmailRepository blastEmailRepository,
            IEmailHelper emailHelper,
            UnitOfWork unitOfWork
        )
        {
            this.userRepository = userRepository;
            this.candidateRepository = candidateRepository;
            this.blastEmailRepository = blastEmailRepository;
            this.emailHelper = emailHelper;
            this.unitOfWork = unitOfWork;
        }

        public List<User> GetUserProfileForBlastEmail()
        {
            return userRepository.GetUserProfileForBlastEmail();
        }

        public bool SendBlastingEmail(string Subject, string Body)
        {
            try
            {
                bool result = false;
                var Recipients = candidateRepository.GetActiveCandidatesEmail();

                if (Recipients.Count > 0)
                {
                    unitOfWork.Run((r, ctx) =>
                    {
                        r.ConvertContextOfRepository(blastEmailRepository).ToUse(ctx);
                        BlastEmailDTO blastEmailDTO = new BlastEmailDTO
                        {
                            IDBlastEmail = new Guid(),
                            IDPeriod = Recipients.Select(x => x.IDPeriod).FirstOrDefault(),
                            Subject = Subject,
                            Description = Body,
                            BlastDateTime = DateTime.Now
                        };
                        blastEmailRepository.Insert(blastEmailDTO);

                        result = emailHelper.Send(new Email
                        {
                            Recipients = Recipients.Select(x => x.RecipientEmail).Distinct().ToList(),
                            Subject = Subject,
                            Body = Body,
                            IsBodyHtml = true
                        });
                    });

                    return result;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
