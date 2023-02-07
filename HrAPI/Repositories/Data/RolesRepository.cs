using HrAPI.Context;
using HrAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class RolesRepository : GeneralRepository<DbContext, Roles, string>
    {
        private readonly MyContext myContext;

        public RolesRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
