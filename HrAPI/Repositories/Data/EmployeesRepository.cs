using BC = BCrypt.Net.BCrypt;
using HrAPI.Context;
using HrAPI.Model;
using HrAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class EmployeesRepository : GeneralRepository<MyContext, Employees, string>
    {
        private readonly MyContext myContext;
        public EmployeesRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public string GenerateNIK()
        {
            var lastNIk = "";
            var newNIK = myContext.Employees.ToList().Count() + 1;

            if (newNIK >= 1 && newNIK <= 9)
            {
                lastNIk = "000" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 10 && newNIK <= 99)
            {
                lastNIk = "00" + Convert.ToString(newNIK);
            }
            else if (newNIK >= 100 && newNIK <= 999)
            {
                lastNIk = "0" + Convert.ToString(newNIK);
            }

            DateTime dateTime = DateTime.UtcNow.Date;
            lastNIk = dateTime.ToString("yyyyddMM") + lastNIk;
            return lastNIk;
        }
        public int Register(RegisterVm registerVm)
        {
            var newNIK = GenerateNIK();
            if(myContext.Employees.SingleOrDefault(e => e.Email == registerVm.Email) != null)
            {
                return 2;
            }
            else if (myContext.Employees.SingleOrDefault(e => e.Phone == registerVm.Phone) != null)
            {
                return 3;
            }
            var empl = new Employees();
            empl.NIK = newNIK;
            empl.FirstName = registerVm.FirstName;
            empl.LastName = registerVm.LastName;
            empl.BirthDate = registerVm.BirthDate;
            empl.Phone = registerVm.Phone;
            empl.Salary = registerVm.Salary;
            empl.Email = registerVm.Email;
            empl.Gender = (Model.Gender)registerVm.Gender;
            empl.Departement_Id = registerVm.Departement_Id;
            myContext.Add(empl);
            myContext.SaveChanges();

            var acc = new Accounts();
            acc.NIK = empl.NIK;
            acc.Password = BC.HashPassword(registerVm.Password);
            myContext.Add(acc);
            myContext.SaveChanges();

            var accRole = new AccountRoles();
            accRole.RoleId = registerVm.Role_Id;
            accRole.AccountNIK = empl.NIK;
            myContext.Add(accRole);
            var response = myContext.SaveChanges();

            return response;
        }
        //public IEnumerable<EmployeeVM> GetEmployee()
        //{
        //    var getEmployee = (from emp in myContext.Employees
        //                       join role in myContext.Roles
        //                       on emp.NIK equals mngr.Id
        //                       where mngr.nam)
        //}

    }
}
