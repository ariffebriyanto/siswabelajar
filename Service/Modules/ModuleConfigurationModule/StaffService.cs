using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.ModuleConfigurationSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.ModuleConfigurationModule
{
    public interface IStaffService
    {
        List<Role> GetRoles();
        string GetRoleNameById(int IDRole);
        List<Module> GetModuleByIdRole(int IDRole);
        Module GetModuleByIdModule(Guid IDModule);
        bool InsertModule(Module module);
        bool UpdateModule(Module module);
        bool DeleteModule(Guid IDModule);
    }
    public class StaffService : IStaffService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IModuleRepository moduleRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(
            IRoleRepository roleRepository,
            IModuleRepository moduleRepository,
            UnitOfWork unitOfWork
        )
        {
            this.roleRepository = roleRepository;
            this.moduleRepository = moduleRepository;
            this.unitOfWork = unitOfWork;
        }

        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            var allRoles = roleRepository.FindAll().ToList();
            foreach (var item in allRoles)
            {
                roles.Add(new Role()
                {
                    IDRole = item.IDRole,
                    RoleName = item.RoleName,
                    RoleLevel = item.RoleLevel
                });
            }
            return roles;
        }

        public string GetRoleNameById(int IDRole)
        {
            return roleRepository.GetRoleById(IDRole).RoleName.ToString();
        }

        public List<Module> GetModuleByIdRole(int IDRole)
        {
            List<Module> modules = new List<Module>();
            var allModules = moduleRepository.GetModuleByIdRole(IDRole);
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

        public Module GetModuleByIdModule(Guid IDModule)
        {
            var moduleById = moduleRepository.GetModuleById(IDModule);
            Module module = new Module();
            module.IDModule = IDModule;
            module.IDRole = moduleById.IDRole;
            module.ModuleName = moduleById.ModuleName;
            module.ModuleLevel = moduleById.ModuleLevel;
            module.Route = moduleById.Route;
            return module;
        }

        public bool InsertModule(Module module)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(moduleRepository).ToUse(ctx);
                    ModuleDTO moduleDTO = new ModuleDTO
                    {
                        IDModule = Guid.NewGuid(),
                        IDRole = module.IDRole,
                        ModuleName = module.ModuleName.Trim(),
                        ModuleLevel = module.ModuleLevel,
                        Route = module.Route.Trim()
                    };
                    moduleRepository.Insert(moduleDTO);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateModule(Module module)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(moduleRepository).ToUse(ctx);
                    var moduleById = moduleRepository.GetModuleById(module.IDModule);
                    moduleById.IDRole = module.IDRole;
                    moduleById.ModuleName = module.ModuleName.Trim();
                    moduleById.ModuleLevel = module.ModuleLevel;
                    moduleById.Route = module.Route.Trim();
                    moduleRepository.Update(moduleById);
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteModule(Guid IDModule)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(moduleRepository).ToUse(ctx);
                    var moduleById = moduleRepository.GetModuleById(IDModule);
                    moduleRepository.Delete(moduleById);
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