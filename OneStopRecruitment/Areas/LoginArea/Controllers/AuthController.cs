using Microsoft.AspNetCore.Mvc;
using Model.DBConstraint;
using Model.Subdomains.LoginSubdomain;
using OneStopRecruitment.Areas.LoginArea.ViewModels.Auth;
using OneStopRecruitment.Helpers.HttpExtensions;
using Service.Modules.LoginModule;

namespace OneStopRecruitment.Areas.LoginArea.Controllers
{
    [Area("LoginArea")]
    public class AuthController : Controller
    {
        private readonly IMainService mainService;
        public AuthController(IMainService mainService)
        {
            this.mainService = mainService;
        }

        public IActionResult Index()
        {

            var LoggedIn = HttpContext.Session.GetLoggedinUser();

            if (LoggedIn != null)
            {
                if (LoggedIn.IDRole == BaseConstraint.Role.Staff.Id)
                {
                    return RedirectToAction("Index", "Staff", new { area = "DashboardArea" });
                }
                else if (LoggedIn.IDRole == BaseConstraint.Role.Candidate.Id)
                {
                    return RedirectToAction("Index", "Candidate", new { area = "DashboardArea" });
                }
                else if (LoggedIn.IDRole == BaseConstraint.Role.Trainer.Id)
                {
                    return RedirectToAction("Index", "Trainer", new { area = "DashboardArea" });
                }
            }

            TempData["IsOpenRegistration"] = mainService.IsOpenRegistration();
            return View();
        }

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                //DELETE THIS
                if (loginViewModel.Login.Username == "admin" && loginViewModel.Login.Password == "admin") 
                {

                    User user = mainService.GetUserLogin(loginViewModel.Login.Username.Trim().ToLower(), loginViewModel.Login.Password);

                    HttpContext.Session.SetLoggedinUser(new Helpers.HttpExtensions.Login
                    {
                        IDUser = user.IDUser,
                        Username = user.Username.Trim().ToLower(),
                        Name = user.Name.Trim(),
                        Email = user.Email.Trim(),
                        IDRole = user.IDRole,
                        RoleName = user.RoleName
                    });

                    return RedirectToAction("Index", user.RoleName, new { area = "DashboardArea" });
                }
                else if (loginViewModel.Login.Username == null)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Login.EmptyUsername;
                    return RedirectToAction("Index");
                }
                else if (loginViewModel.Login.Password == null)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Login.EmptyPassword;
                    return RedirectToAction("Index");
                }

                bool isUserExist = mainService.GetUserAvailability(loginViewModel.Login.Username.Trim().ToLower());
                if (!isUserExist)
                {
                    TempData["ErrorMessage"] = AlertConstraint.Login.UsernameNotFound;
                    return RedirectToAction("Index");
                }
                else 
                {
                    User user = mainService.GetUserLogin(loginViewModel.Login.Username.Trim().ToLower(), loginViewModel.Login.Password);
                    if (user == null)
                    {
                        TempData["ErrorMessage"] = AlertConstraint.Login.InvalidPassword;
                        return RedirectToAction("Index");
                    }

                    // Check if the user is candidate then check if the user is registered in current active period
                    else if (user.IDRole != 2 || (user.IDRole == 2 && mainService.IsActiveCandidate(user.Username)))
                    {
                        HttpContext.Session.SetLoggedinUser(new Helpers.HttpExtensions.Login
                        {
                            IDUser = user.IDUser,
                            Username = user.Username.Trim().ToLower(),
                            Name = user.Name.Trim(),
                            Email = user.Email.Trim(),
                            IDRole = user.IDRole,
                            RoleName = user.RoleName
                        });

                        return RedirectToAction("Index", user.RoleName, new { area = "DashboardArea" });
                    }
                    else 
                    {
                        TempData["ErrorMessage"] = AlertConstraint.Login.InvalidPeriod;
                        return RedirectToAction("Index");
                    }
                }

                TempData["ErrorMessage"] = AlertConstraint.Default.Error;
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = AlertConstraint.Default.Error;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
