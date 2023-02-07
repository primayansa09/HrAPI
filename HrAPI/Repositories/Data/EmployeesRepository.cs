using BC = BCrypt.Net.BCrypt;
using HrAPI.Context;
using HrAPI.Model;
using HrAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace HrAPI.Repositories.Data
{
    public class EmployeesRepository : GeneralRepository<DbContext, Employees, string>
    {
        private readonly MyContext myContext;
        public EmployeesRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
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
        public int Reagister(RegisterVm registerVm)
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

            var depart = new Departements();
            depart.Manager_Id = registerVm.Manager_Id;
            myContext.Add(depart);
            myContext.SaveChanges();

            var accRole = new AccountRoles();
            accRole.RoleId = registerVm.Role_Id;
            accRole.AccountNIK = empl.NIK;
            myContext.Add(accRole);
            var response = myContext.SaveChanges();

            //var generatePassword = RandomString(10);
           

            return response;
        }
     
    }
}
