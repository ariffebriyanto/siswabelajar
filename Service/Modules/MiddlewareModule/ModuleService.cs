using Model.Subdomains.MiddlewareSubdomain;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;

namespace Service.Modules.MiddlewareModule
{
    public interface IModuleService
    {
        List<Module> GetModuleByUrl(string URL);
    }

    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository moduleRepository;
        public ModuleService(IModuleRepository moduleRepository)
        {
            this.moduleRepository = moduleRepository;
        }

        public List<Module> GetModuleByUrl(string URL)
        {
            List<Module> modules = new List<Module>();
            var allModules = moduleRepository.GetModuleByURL(URL);
            foreach (var item in allModules)
            {
                Module module = new Module();
                module.IDModule = item.IDModule;
                module.ModuleName = item.ModuleName;
                module.ModuleLevel = item.ModuleLevel;
                module.IDRole = item.IDRole;
                module.Route = item.Route;
                modules.Add(module);
            }
            return modules;
        }
    }
}
