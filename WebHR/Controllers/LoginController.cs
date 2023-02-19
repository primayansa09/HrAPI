using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHR.Context;
using WebHR.ViewModel;

namespace WebHR.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public async Task<IActionResult> Login(VMLogin vMLogin)
        //{
        //    var userLogin = await myContext.AccountRoles
        //        .Where(ar => ar.Accounts.Employees.Email == vMLogin.Email)
        //        .Include(r => r.Roles).Include(e => e.Accounts.Employees)
        //        .FirstOrDefaultAsync();
        //    if (userLogin == null || !BC.Verify(vMLogin.Password, userLogin.Accounts.Password))
        //    {
        //        ViewBag.Message = "NIK or Password is Valid";

        //        return View();
        //    }
        //    HttpContext.Session.SetString("Email", userLogin.Accounts.Employees.Email);
        //    HttpContext.Session.SetString("Name", userLogin.Accounts.Employees.FirstName + " " + userLogin.Accounts.Employees.LastName);
        //    HttpContext.Session.SetString("Role", userLogin.Roles.Name);
        //    if(HttpContext.Session.GetString("Role") == "Employee")
        //    {
        //        return RedirectToAction("Index", "Dashboard", new { area = "" });
        //    }
        //    return RedirectToAction("Index", "Dashboard", new { area = "" });
        //}
    }
}
