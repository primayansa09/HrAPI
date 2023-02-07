using BC = BCrypt.Net.BCrypt;
using HrAPI.Context;
using HrAPI.Model;
using HrAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace HrAPI.Repositories.Data
{
    public class AccountsRepository : GeneralRepository<DbContext, Accounts, string>
    {
        private readonly MyContext myContext;
        private IConfiguration _configuration;
        public AccountsRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }
        public async Task<string> Login([FromForm]LoginVm loginVm)
        {
            var response = await myContext.AccountRoles
                .Where(ar => ar.Accounts.Employees.Email == loginVm.Email
                && ar.Accounts.Password == loginVm.Password)
                .Include(r => r.Roles).Include(e => e.Accounts.Employees)
                .FirstOrDefaultAsync();
            if (loginVm.Password == null)
            {
                return "400";
            }
            else if (response != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("Email", response.Accounts.Employees.Email),
                    new Claim("roles", response.Roles.Name)
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: credential);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
            }
            else
            {
                return "404";
            }


            //var response = myContext.Accounts.SingleOrDefault(a => a.NIK == loginVm.NIK);
            //if (response == null || !BC.Verify(loginVm.Password, response.Password))
            //{
            //    return null;
            //}

            //var logging = new LoginVm();
            //logging.Email = response.Accounts.Employees.Email;
            //logging.Password = response.Accounts.Password;
            //return logging;
            
        }
    }
}
