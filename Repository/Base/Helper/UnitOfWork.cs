using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Base.Helper
{
    public class UnitOfWork
	{
		private readonly IServiceProvider serviceProvider;

		public UnitOfWork(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public async Task RunAsync(RepoWorkAsync repoWork)
		{
			RepositoryRegistry rRegistry = new RepositoryRegistry();
			RepositoryRegistryRegistrar rRegistrar = new RepositoryRegistryRegistrar(rRegistry);
			OneStopRecruitmentDbContext localContext = null;
			try
			{
				using (var scope = serviceProvider.CreateScope())
				{
					localContext = scope.ServiceProvider.GetService<OneStopRecruitmentDbContext>();
					localContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
					using (var trans = localContext.Database.BeginTransaction())
					{
						try
						{
							await repoWork(rRegistrar, localContext);
							trans.Commit();
						}
						catch (Exception e)
						{
							trans.Rollback();
							throw e;
						}
					}

				}
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				rRegistry.GetRepositories().ForEach(unitOfWorkRepository => unitOfWorkRepository.RevertToPreviousDbContext());
				rRegistry.Clear();
			}
		}

		public void Run(RepoWork repoWork)
		{
			RepositoryRegistry rRegistry = new RepositoryRegistry();
			RepositoryRegistryRegistrar rRegistrar = new RepositoryRegistryRegistrar(rRegistry);
			OneStopRecruitmentDbContext localContext = null;
			try
			{
				using (var scope = serviceProvider.CreateScope())
				{
					localContext = scope.ServiceProvider.GetService<OneStopRecruitmentDbContext>();
					localContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
					using (var trans = localContext.Database.BeginTransaction())
					{
						try
						{
							repoWork(rRegistrar, localContext);
							trans.Commit();
						}
						catch (Exception e)
						{
							trans.Rollback();
							throw e;
						}
					}

				}
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				rRegistry.GetRepositories().ForEach(unitOfWorkRepository => unitOfWorkRepository.RevertToPreviousDbContext());
				rRegistry.Clear();
			}
		}

		public delegate Task RepoWorkAsync(RepositoryRegistryRegistrar rRegistrar, OneStopRecruitmentDbContext context);
		public delegate void RepoWork(RepositoryRegistryRegistrar rRegistrar, OneStopRecruitmentDbContext context);
	}

	public class RepositoryRegistryRegistrar
	{
        private readonly RepositoryRegistry repositoryRegistry;

		public RepositoryRegistryRegistrar(RepositoryRegistry repositoryRegistry)
		{
			this.repositoryRegistry = repositoryRegistry;
		}

		public RepositoryContextAdapter ConvertContextOfRepository(IUnitOfWorkRepository repository)
		{
			return new RepositoryContextAdapter(repository, repositoryRegistry);
		}
	}

	public class RepositoryContextAdapter
	{
        private readonly IUnitOfWorkRepository repository;
        private readonly RepositoryRegistry repositoryRegistry;

		public RepositoryContextAdapter(IUnitOfWorkRepository repository, RepositoryRegistry repositoryRegistry)
		{
			this.repository = repository;
			this.repositoryRegistry = repositoryRegistry;
		}

		public void ToUse(OneStopRecruitmentDbContext context)
		{
			repositoryRegistry.AddRepo(repository);
			repository.UseContext(context);
		}
	}

	public class RepositoryRegistry
	{
		private readonly List<IUnitOfWorkRepository> Repositories = new List<IUnitOfWorkRepository>();
		public void AddRepo(IUnitOfWorkRepository repository) { Repositories.Add(repository); }
		public List<IUnitOfWorkRepository> GetRepositories() { return Repositories; }
		public void Clear() { Repositories.Clear(); }
	}
}
