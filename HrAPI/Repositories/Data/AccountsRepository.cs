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
    public class AccountsRepository : GeneralRepository<MyContext, Accounts, string>
    {
        private readonly MyContext myContext;
        private IConfiguration _configuration;
        public AccountsRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }
        public async Task<LoginToken> Login([FromBody] LoginVm loginVm)
        {
            var response = await myContext.AccountRoles
                .Where(ar => ar.Accounts.Employees.Email == loginVm.Email)
                .Include(r => r.Roles).Include(e => e.Accounts.Employees)
                .FirstOrDefaultAsync();

            if (response == null || !BC.Verify(loginVm.Password, response.Accounts.Password))
            {
                return null;
            }

            // JWT Tokens
            var timeNow = DateTime.Now;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var tokenDiscription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, response.Accounts.Employees.FirstName + " " + response.Accounts.Employees.LastName),
                    new Claim(ClaimTypes.Email, response.Accounts.Employees.Email),
                    new Claim(ClaimTypes.Role, response.Roles.Name)
                }),
                Expires = timeNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)

            };

            var token = tokenHandler.CreateToken(tokenDiscription);

            var loggedUser = new LoginToken();
            loggedUser.NIK = response.Accounts.NIK;
            loggedUser.Email = response.Accounts.Employees.Email;
            loggedUser.Password = response.Accounts.Password;
            loggedUser.Role = response.Roles.Name;
            loggedUser.DepartementId = response.Accounts.Employees.Departement_Id;
            loggedUser.Token = tokenHandler.WriteToken(token);
            loggedUser.TokenExpires = timeNow.AddMinutes(15);
            return loggedUser;

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //        new Claim(ClaimTypes.Email, response.Accounts.Employees.Email),
            //        new Claim(ClaimTypes.Role, response.Roles.Name)
            //    };

            //var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            //        _configuration["Jwt:Audience"],
            //        claims,
            //        expires: DateTime.Now.AddMinutes(15),
            //        signingCredentials: credential);

            //var jwt = tokenHandler.WriteToken(token);

        }
    }
}
