using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using OneStopRecruitment.Helpers.HttpExtensions;
using OneStopRecruitment.Helpers.SmtpHelpers;
using OneStopRecruitment.Middlewares.ActionFilters;
using Repository.Base.Helper;
using Repository.Context;
using Repository.Repositories.OneStopRecruitmentRepository;
using Repository.Repositories.OracleRepository;
using Service.Helpers.EmailHelper;
using Service.Modules;
using System;
using System.IO;

namespace OneStopRecruitment
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = ".UniInternship.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDbContextFactory, DbContextFactory>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddDbContext<OneStopRecruitmentDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OneStopRecruitmentDbConnection")));
            services.AddDbContext<OracleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OracleDbConnection")));
            services.AddTransient<OneStopRecruitmentDbContext>();
            services.AddTransient<OracleDbContext>();
            services.AddTransient<UnitOfWork>();
            services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            var mvcBuilder = services.AddControllersWithViews();
            mvcBuilder.AddRazorRuntimeCompilation();

            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddAreaPageRoute("Dashboard", "/Dashboard/Candidate", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Helpers
            services.AddTransient<IApplicationSessionDataAccessor, ApplicationSessionDataAccessor>();

            // Middleware
            services.AddTransient<AuthenticationActionFilter>();
            services.AddTransient<SetCurrentMenuOrRedirectActionFilter>();

            // Repositories
            services.AddTransient<IPS_N_PERSONCAR_TBLRepository, PS_N_PERSONCAR_TBLRepository>();
            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            services.AddTransient<ILogicTestQuestionTypeRepository, LogicTestQuestionTypeRepository>();
            services.AddTransient<IMasterLogicTestQuestionRepository, MasterLogicTestQuestionRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IPeriodRepository, PeriodRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITransactionLogicTestQuestionRepository, TransactionLogicTestQuestionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStageRepository, StageRepository>();
            services.AddTransient<ISubStageRepository, SubStageRepository>();
            services.AddTransient<IMinimumScoreRepository, MinimumScoreRepository>();
            services.AddTransient<IScoringComponentTypeRepository, ScoringComponentTypeRepository>();
            services.AddTransient<IScoringComponentRepository, ScoringComponentRepository>();
            services.AddTransient<IMasterScheduleRepository, MasterScheduleRepository>();
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<ILogicTestAnswerRepository, LogicTestAnswerRepository>();
            services.AddTransient<ICandidateScoreRepository, CandidateScoreRepository>();
            services.AddTransient<ICandidateDraftRepository, CandidateDraftRepository>();
            services.AddTransient<ITransactionScheduleRepository, TransactionScheduleRepository>();
            services.AddTransient<IBlastEmailRepository, BlastEmailRepository>();
            services.AddTransient<ISubmissionRepository, SubmissionRepository>();
            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();

            // Services
            // Staff Service
            services.AddTransient<Service.Modules.AssignmentModule.IStaffService, Service.Modules.AssignmentModule.StaffService>();
            services.AddTransient<Service.Modules.BlastEmailModule.IStaffService, Service.Modules.BlastEmailModule.StaffService>();
            services.AddTransient<Service.Modules.DashboardModule.IStaffService, Service.Modules.DashboardModule.StaffService>();
            services.AddTransient<Service.Modules.LayoutModule.IMainService, Service.Modules.LayoutModule.MainService>();
            services.AddTransient<Service.Modules.LoginModule.IMainService, Service.Modules.LoginModule.MainService>();
            services.AddTransient<Service.Modules.MasterLogicTestModule.IStaffService, Service.Modules.MasterLogicTestModule.StaffService>();
            services.AddTransient<Service.Modules.MasterPeriodModule.IStaffService, Service.Modules.MasterPeriodModule.StaffService>();
            services.AddTransient<Service.Modules.MiddlewareModule.IModuleService, Service.Modules.MiddlewareModule.ModuleService>();
            services.AddTransient<Service.Modules.ModuleConfigurationModule.IStaffService, Service.Modules.ModuleConfigurationModule.StaffService>();
            services.AddTransient<Service.Modules.RegistrationModule.IMainService, Service.Modules.RegistrationModule.MainService>();            
            services.AddTransient<Service.Modules.StageModule.IStaffService, Service.Modules.StageModule.StaffService>();
            services.AddTransient<Service.Modules.SubStageModule.IStaffService, Service.Modules.SubStageModule.StaffService>();
            services.AddTransient<Service.Modules.MinimumScoreModule.IStaffService, Service.Modules.MinimumScoreModule.StaffService>();
            services.AddTransient<Service.Modules.MasterScoringComponentModule.IStaffService, Service.Modules.MasterScoringComponentModule.StaffService>();
            services.AddTransient<Service.Modules.MasterScheduleModule.IStaffService, Service.Modules.MasterScheduleModule.StaffService>();
            services.AddTransient<Service.Modules.MasterCandidateModule.IStaffService, Service.Modules.MasterCandidateModule.StaffService>();
            services.AddTransient<Service.Modules.UserModule.IStaffService, Service.Modules.UserModule.StaffService>();

            // Candidate Service
            services.AddTransient<Service.Modules.DashboardModule.ICandidateService, Service.Modules.DashboardModule.CandidateService>();
            services.AddTransient<Service.Modules.AssignmentModule.ICandidateService, Service.Modules.AssignmentModule.CandidateService>();
            services.AddTransient<Service.Modules.MasterLogicTestModule.ICandidateService, Service.Modules.MasterLogicTestModule.CandidateService>();

            // Trainer Service            
            services.AddTransient<Service.Modules.DashboardModule.ITrainerService, Service.Modules.DashboardModule.TrainerService>();
            services.AddTransient<Service.Modules.AssignmentModule.ITrainerService, Service.Modules.AssignmentModule.TrainerService>();
            services.AddTransient<Service.Modules.ScoreModule.ITrainerService, Service.Modules.ScoreModule.TrainerService>();

            // SMTP
            services.AddSingleton<IEmailHelper, EmailHelper>();


            ////start arif
            services.AddControllers();

            services.AddScoped<IMyScopedService, MyScopedService>();

            services.AddCronJob<MyCronJob1>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"* * * * *";

            });


            ///// end arif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"AppData/Uploads/LogicTest/Answers")),
                RequestPath = new PathString("/AppData/Uploads/LogicTest/Answers")
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"AppData/Uploads/LogicTest/Questions")),
                RequestPath = new PathString("/AppData/Uploads/LogicTest/Questions")
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "" || context.Request.Path == "/")
                {
                    context.Response.Redirect("/Login/", true);
                    return;
                }
                await next();
            });

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "LoginArea",
                    areaName: "LoginArea",
                    template: "Login/{controller=Auth}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "RegistrationArea",
                    areaName: "RegistrationArea",
                    template: "Registration/{controller=Main}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "DashboardArea",
                    areaName: "DashboardArea",
                    template: "Dashboard/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "ModuleConfigurationArea",
                    areaName: "ModuleConfigurationArea",
                    template: "ModuleConfiguration/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MasterPeriodArea",
                    areaName: "MasterPeriodArea",
                    template: "MasterPeriod/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MasterLogicTestArea",
                    areaName: "MasterLogicTestArea",
                    template: "MasterLogicTest/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "StageArea",
                    areaName: "StageArea",
                    template: "Stage/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "SubstageArea",
                    areaName: "SubstageArea",
                    template: "Substage/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MinimumScoreArea",
                    areaName: "MinimumScoreArea",
                    template: "MinimumScore/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MasterScoringComponentArea",
                    areaName: "MasterScoringComponentArea",
                    template: "MasterScoringComponent/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MasterScheduleArea",
                    areaName: "MasterScheduleArea",
                    template: "MasterSchedule/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "MasterCandidateArea",
                    areaName: "MasterCandidateArea",
                    template: "MasterCandidate/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "AssignmentArea",
                    areaName: "AssignmentArea",
                    template: "Assignment/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "UserArea",
                    areaName: "UserArea",
                    template: "User/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "ScoreArea",
                    areaName: "ScoreArea",
                    template: "Score/{controller}/{action=Index}"
                );
                routes.MapAreaRoute(
                    name: "BlastEmailArea",
                    areaName: "BlastEmailArea",
                    template: "BlastEmail/{controller}/{action=Index}"
                );

                // Ini harus di paling bawah
                routes.MapRoute(
                    name: "PageNotFound",
                    template: "{controller=PageNotFound}/{action=Index}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}"
                );
            });

            app.Run(async (context) =>
            {
                context.Response.Redirect("/PageNotFound/", false);
            });
        }
    }
}