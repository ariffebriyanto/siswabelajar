using Model.DTO.OneStopRecruitmentDTO;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplateDTO>
    {

    }
    public class EmailTemplateRepository : BaseRepository<EmailTemplateDTO>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
