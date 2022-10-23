using Helper;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.UserSubdomain;
using Repository.Base.Helper;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.UserModule
{
    public interface IStaffService
    {
        List<Role> GetRoles();
        List<User> GetUsersByRoleID(int IDRole);
        User GetUserByID(Guid IDUser);
        bool CheckUsername(string username);
        bool CheckPassword(User user);
        bool UpdatePassword(User user);
        bool InsertUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(Guid IDUser);

    }

    public class StaffService : IStaffService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly UnitOfWork unitOfWork;

        public StaffService(IUserRepository userRepository, IRoleRepository roleRepository, UnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
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

        public List<User> GetUsersByRoleID(int IDRole)
        {
            List<UserDTO> usersDTO = userRepository.FindAll().Where(x => x.IDRole == IDRole).ToList();            
            List<User> result = new List<User>();
            
            foreach(var item in usersDTO)
            {
                result.Add(new User()
                {
                    IDUser = item.IDUser,
                    Email = item.Email,
                    Username = item.Username,
                    IDRole = item.IDRole,
                    Name = item.Name.ToUpper()
                });
            }

            return result;
        }

        public User GetUserByID(Guid IDUser)
        {
            UserDTO user = userRepository.GetUserByUserId(IDUser);
            if (user == null)
            {
                return null;
            }

            return new User()
            {
                IDUser = user.IDUser,
                IDRole = user.IDRole,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username
            };
        }

        public bool CheckUsername(string username)
        {
            UserDTO dto = userRepository.GetUserByUsername(username);
            if (dto != null && !dto.IDUser.Equals(Guid.Empty))
            {
                return true;
            }

            return false;
        }

        public bool CheckPassword(User user)
        {
            UserDTO dto = userRepository.GetUserByUserId(user.IDUser);
            if (dto == null)
            {
                return false;
            }

            return dto.Password.Equals(SHA256Encryption.Encrypt(user.OldPassword));
        }

        public bool UpdatePassword(User user)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(userRepository).ToUse(ctx);

                    UserDTO dto = userRepository.GetUserByUserId(user.IDUser);
                    dto.Password = SHA256Encryption.Encrypt(user.Password);

                    userRepository.Update(dto);                    
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertUser(User user)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(userRepository).ToUse(ctx);

                    UserDTO dto = new UserDTO()
                    {
                        Name = user.Name,
                        Email = user.Email,
                        IDRole = user.IDRole,
                        Username = user.Username,
                        Password = SHA256Encryption.Encrypt(user.Password)
                    };                 

                    userRepository.Insert(dto);
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {                
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(userRepository).ToUse(ctx);
                    
                    UserDTO dto = userRepository.GetUserByUserId(user.IDUser);
                    dto.Name = user.Name;
                    dto.Email = user.Email;
                    dto.IDRole = user.IDRole;
                        
                    userRepository.Update(dto);
                        
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(Guid IDUser)
        {
            try
            {
                unitOfWork.Run((r, ctx) =>
                {
                    r.ConvertContextOfRepository(userRepository).ToUse(ctx);

                    UserDTO user = userRepository.GetUserByUserId(IDUser);
                    userRepository.Delete(user);
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
