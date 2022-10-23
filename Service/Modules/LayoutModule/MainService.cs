using Helper.StringHelper;
using Model.Subdomains.LayoutSubdomain;
using Repository.Repositories.OneStopRecruitmentRepository;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.LayoutModule
{
    public interface IMainService
    {
        List<SideBarItem> GetModulesByRole(int IDRole);
    }
    public class MainService : IMainService
    {
        private readonly IModuleRepository moduleRepository;

        public MainService(IModuleRepository moduleRepository)
        {
            this.moduleRepository = moduleRepository;
        }

        public List<SideBarItem> GetModulesByRole(int IDRole)
        {
            List<SideBarItem> modules = new List<SideBarItem>();
            var allModules = moduleRepository.FindAll().Where(x => x.IDRole.Equals(IDRole)).OrderBy(x => x.ModuleLevel).ToList();
            foreach (var item in allModules)
            {
                try
                {
                    List<string> splittedRoute = StringManipulation.SplitToList(item.Route, ";");
                    List<string> splittedFirstRoute = StringManipulation.SplitToList(splittedRoute[0], "/");

                    SideBarItem sideBarItem = new SideBarItem();
                    sideBarItem.IDModule = item.IDModule;
                    sideBarItem.ModuleName = item.ModuleName;
                    sideBarItem.ModuleLevel = item.ModuleLevel;
                    sideBarItem.ModuleArea = splittedFirstRoute[0] + "Area";
                    sideBarItem.ModuleController = splittedFirstRoute[1];
                    sideBarItem.ModuleAction = splittedFirstRoute[2];
                    modules.Add(sideBarItem);
                }
                catch
                {

                }
            }
            return modules;
        }
    }
}