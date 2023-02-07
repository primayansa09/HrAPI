using HrAPI.Context;
using HrAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class AccountRolesRepository : GeneralRepository<DbContext, AccountRoles, string>
    {
        private readonly MyContext myContext;

        public AccountRolesRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
