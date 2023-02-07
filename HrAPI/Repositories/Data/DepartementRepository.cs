using HrAPI.Context;
using HrAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class DepartementRepository : GeneralRepository<DbContext, Departements, string>
    {
        private readonly MyContext myContext;
        public DepartementRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
