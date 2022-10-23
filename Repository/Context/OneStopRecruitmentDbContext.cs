using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.DTO.Base;
using Model.DTO.OneStopRecruitmentDTO;
using System;
using System.Collections.Generic;
using static Model.DBConstraint.BaseConstraint;

namespace Repository.Context
{
    public class OneStopRecruitmentDbContext : DbContext
	{
		private readonly IApplicationSessionDataAccessor applicationSessionDataAccessor;
		public OneStopRecruitmentDbContext(DbContextOptions<OneStopRecruitmentDbContext> options, IApplicationSessionDataAccessor applicationSessionDataAccessor) : base(options)
		{
			this.applicationSessionDataAccessor = applicationSessionDataAccessor;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
		}

		public DbSet<AssignmentDTO> assignmentDTOs { get; set; }
		public DbSet<LogicTestQuestionTypeDTO> logicTestQuestionTypeDTOs { get; set; }
		public DbSet<MasterLogicTestQuestionDTO> masterLogicTestQuestionDTOs { get; set; }
		public DbSet<ModuleDTO> moduleDTOs { get; set; }
		public DbSet<PeriodDTO> periodDTOs { get; set; }
		public DbSet<PositionDTO> positionDTOs { get; set; }
		public DbSet<RoleDTO> roleDTOs { get; set; }
		public DbSet<StageDTO> stageDTOs { get; set; }
		public DbSet<SubStageDTO> subStageDTOs { get; set; }
		public DbSet<TransactionLogicTestQuestionDTO> transactionLogicTestQuestionDTOs { get; set; }
		public DbSet<UserDTO> userDTOs { get; set; }
		public DbSet<MinimumScoreDTO> minimumScoreDTOs { get; set; }
		public DbSet<ScoringComponentTypeDTO> scoringComponentTypeDTOs { get; set; }
		public DbSet<ScoringComponentDTO> scoringComponentDTOs { get; set; }
		public DbSet<MasterScheduleDTO> masterScheduleDTOs { get; set; }
		public DbSet<CandidateDTO> candidateDTOs { get; set; }
		public DbSet<LogicTestAnswerDTO> logicTestAnswerDTOs { get; set; }
		public DbSet<CandidateScoreDTO> candidateScoreDTOs { get; set; }
		public DbSet<CandidateDraftDTO> candidateDraftDTOs { get; set; }
		public DbSet<TransactionScheduleDTO> transactionScheduleDTOs { get; set; }
		public DbSet<BlastEmailDTO> blastEmailDTOs { get; set; }
		public DbSet<SubmissionDTO> submissionDTOs { get; set; }
		public DbSet<EmailTemplateDTO> emailTemplateDTOs { get; set; }

		public override int SaveChanges()
		{
			OnBeforeSave();
			return base.SaveChanges();
		}

		public int SaveDeletion()
		{
			OnBeforeDelete();
			return base.SaveChanges();
		}

		public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
		{
			Entry(entity).State = EntityState.Deleted;
			return base.Update(entity);
		}

		private void OnBeforeSave()
		{
			IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is BaseModel model)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							model.SetUserIn(applicationSessionDataAccessor.GetLoginUserName());
							model.SetDateIn(DateTime.Now);
							model.SetDateUp(null);
							model.SetUserUp(null);
							model.SetStsRc(StsRc.Active);
							break;
						case EntityState.Modified:
							model.SetUserUp(applicationSessionDataAccessor.GetLoginUserName());
							model.SetDateUp(DateTime.Now);
							model.SetStsRc(StsRc.Active);
							break;
						case EntityState.Deleted:
							model.SetUserUp(applicationSessionDataAccessor.GetLoginUserName());
							model.SetDateUp(DateTime.Now);
							model.SetStsRc(StsRc.Delete);
							break;
					}
				}
			}
		}

		private void OnBeforeDelete()
		{
			IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				if (entry.Entity is BaseModel model)
				{
					model.SetUserUp(applicationSessionDataAccessor.GetLoginUserName());
					model.SetDateUp(DateTime.Now);
					model.SetStsRc(StsRc.Delete);
				}
			}
		}
	}
}