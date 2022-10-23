using Model.DTO.OracleDTO;
using Repository.Base;
using Repository.Context;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories.OracleRepository
{
    public interface IPS_N_PERSONCAR_TBLRepository : IOracleRepository<PS_N_PERSONCAR_TBLDTO>
    {
        PS_N_PERSONCAR_TBLDTO GetUserByEmplid(string Emplid);
        PS_N_PERSONCAR_TBLDTO GetUserByUserInput(string UserInput);
        IEnumerable<PS_N_PERSONCAR_TBLDTO> GetUserByUserInputList(List<string> UserInputList);
    }

    public class PS_N_PERSONCAR_TBLRepository : OracleBaseRepository<PS_N_PERSONCAR_TBLDTO>, IPS_N_PERSONCAR_TBLRepository
    {
        public PS_N_PERSONCAR_TBLRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public PS_N_PERSONCAR_TBLDTO GetUserByEmplid(string Emplid)
        {
            return Context.Set<PS_N_PERSONCAR_TBLDTO>()
                .Where(x => x.EMPLID.Equals(Emplid)
                ).FirstOrDefault();
        }

        public PS_N_PERSONCAR_TBLDTO GetUserByUserInput(string UserInput)
        {
            return Context.Set<PS_N_PERSONCAR_TBLDTO>()
                .Where(x => x.EMPLID.Equals(UserInput) ||
                            x.EXTERNAL_SYSTEM_ID.Equals(UserInput) ||
                            x.N_STDNT_ID2.Equals(UserInput)
                ).FirstOrDefault();
        }

        public IEnumerable<PS_N_PERSONCAR_TBLDTO> GetUserByUserInputList(List<string> UserInputList)
        {
            return Context.Set<PS_N_PERSONCAR_TBLDTO>()
                .Where(x => UserInputList.Contains(x.EMPLID) ||
                            UserInputList.Contains(x.EXTERNAL_SYSTEM_ID) ||
                            UserInputList.Contains(x.N_STDNT_ID2)
                );
        }
    }
}