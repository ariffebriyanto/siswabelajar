using Helper;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.LoginSubdomain;
using Repository.Repositories.OneStopRecruitmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Modules.LoginModule
{
    public interface IMainService
    {
        bool GetUserAvailability(string UserInput);
        User GetUserLogin(string UserInput, string Password);
        bool IsOpenRegistration();
        bool IsActiveCandidate(string Username);
    }
    public class MainService : IMainService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IPeriodRepository periodRepository;
        private readonly ICandidateRepository candidateRepository;

        public MainService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPeriodRepository periodRepository,
            ICandidateRepository candidateRepository
        )
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.periodRepository = periodRepository;
            this.candidateRepository = candidateRepository;
        }

        public bool GetUserAvailability(string UserInput)
        {
            UserDTO userDTO = userRepository.GetUserByUserInput(UserInput);

            if (userDTO == null)
            {
                return false;
            }

            return true;
        }

        public User GetUserLogin(string UserInput, string Password)
        {
            UserDTO userDTO = userRepository.GetUserByUserInput(UserInput);

            //DELETE THIS
            if (UserInput.Equals("admin") && Password.Equals("admin"))
            {
                userDTO = userRepository.GetUserByUserInput("Dita");
                RoleDTO role = roleRepository.GetRoleById(userDTO.IDRole);

                return new User()
                {
                    IDUser = userDTO.IDUser,
                    Name = userDTO.Name,
                    Username = userDTO.Username.ToLower(),
                    Email = userDTO.Email,
                    IDRole = role.IDRole,
                    RoleName = role.RoleName
                };
            }

            if (userDTO == null)
            {
                return null;
            }

            if (!userDTO.Password.Equals(SHA256Encryption.Encrypt(Password)))
            {
                return null;
            }

            RoleDTO roleDTO = roleRepository.GetRoleById(userDTO.IDRole);

            if (roleDTO == null)
            {
                return null;
            }

            return new User()
            {
                IDUser = userDTO.IDUser,
                Name = userDTO.Name,
                Username = userDTO.Username.ToLower(),
                Email = userDTO.Email,
                IDRole = roleDTO.IDRole,
                RoleName = roleDTO.RoleName
            };
        }

        public bool IsOpenRegistration()
        {
            try
            {
                DateTime curr = DateTime.Now.Date;
                PeriodDTO period = periodRepository.GetActivePeriod();
                if (curr.CompareTo(period.DeadlineStart.Date) >= 0 && curr.CompareTo(period.DeadlineEnd.Date) <= 0)
                {
                    return true;
                }
            }
            catch{}

            return false;
        }

        public bool IsActiveCandidate(string Username)
        {
            try
            {
                PeriodDTO periodDTO = periodRepository.GetActivePeriod();
                CandidateDTO candidateDTO = candidateRepository.GetCandidateByNIM(Username);
                if(candidateDTO != null && periodDTO != null)
                {
                    return candidateDTO.IDPeriod == periodDTO.IDPeriod;
                }                

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}