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
        public async Task<string> Login(LoginVm loginVm)
        {
            var response = await myContext.AccountRoles
                .Where(ar => ar.Accounts.Employees.Email == loginVm.Email)
                .Include(r => r.Roles).Include(e => e.Accounts.Employees)
                .FirstOrDefaultAsync();

            if (response == null || !BC.Verify(loginVm.Password, response.Accounts.Password))
            {
                return "400";
            }

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
    }
}
