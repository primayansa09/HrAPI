using HrAPI.Context;
using HrAPI.Model;

namespace HrAPI.Repositories.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Roles, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
