using Helper.StringHelper;
using Model.DBConstraint;
using Model.DTO.OneStopRecruitmentDTO;
using Model.Subdomains.EmailSubdomain;
using Repository.Base;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OneStopRecruitmentRepository
{
    public interface IUserRepository : IRepository<UserDTO>
    {
        UserDTO GetUserByUserId(Guid IDUser);
        UserDTO GetUserByUsername(string Username);
        IEnumerable<UserDTO> GetUserByUsernameList(List<string> UsernameList);
        UserDTO GetUserByUserInput(string UserInput);
        IEnumerable<UserDTO> GetUserByUserIDList(List<Guid> IDUserList);
        IEnumerable<UserDTO> GetUserCandidateList(List<string> Nim, int IDRole);
        List<User> GetUserProfileForBlastEmail();
    }

    public class UserRepository : BaseRepository<UserDTO>, IUserRepository
    {
        public UserRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public UserDTO GetUserByUserId(Guid IDUser)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.IDUser.Equals(IDUser)
                ).FirstOrDefault();
        }

        public UserDTO GetUserByUsername(string Username)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            x.Username.Equals(Username.ToLower())
                ).FirstOrDefault();
        }

        public IEnumerable<UserDTO> GetUserByUsernameList(List<string> UsernameList)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            StringManipulation.ToLowerListString(UsernameList).Contains(x.Username)
                );
        }

        public UserDTO GetUserByUserInput(string UserInput)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            (x.Username.Trim().ToLower().Equals(UserInput.Trim().ToLower()) ||
                            x.Email.Trim().ToLower().Equals(UserInput.Trim().ToLower()))
                ).FirstOrDefault();
        }

        public IEnumerable<UserDTO> GetUserByUserIDList(List<Guid> IDUserList)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            IDUserList.Contains(x.IDUser)
                );
        }

        public IEnumerable<UserDTO> GetUserCandidateList(List<string> Nim, int IDRole)
        {
            return Context.userDTOs
                .Where(x => !x.StsRc.Equals(BaseConstraint.StsRc.Delete) &&
                            !x.StsRc.Equals(BaseConstraint.StsRc.Inactive) &&
                            Nim.Contains(x.Username) && x.IDRole == IDRole
                );
        }

        public List<User> GetUserProfileForBlastEmail()
        {
            return (
                from U in this.Context.userDTOs
                join R in this.Context.roleDTOs on U.IDRole equals R.IDRole
                where !U.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !U.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                where !R.StsRc.Equals(BaseConstraint.StsRc.Delete)
                where !R.StsRc.Equals(BaseConstraint.StsRc.Inactive)
                select new User
                {
                    Name = U.Name,
                    Username = U.Username,
                    Email = U.Email,
                    IDRole = U.IDRole,
                    RoleName = R.RoleName
                }
            ).ToList();
        }
    }
}