using HrAPI.Context;
using HrAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class DepartementRepository : GeneralRepository<MyContext, Departements, int>
    {
   
        public DepartementRepository(MyContext myContext) : base(myContext)
        {
         
        }
    }
}
