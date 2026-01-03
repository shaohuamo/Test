using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace SignWithGoogle.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize("NotAuthorized")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser<Guid>> userManager,
            SignInManager<IdentityUser<Guid>> signInManager, RoleManager<IdentityRole<Guid>> roleManager,
            IConfiguration configuration)
        {
            //通过依赖注入来给_userManager赋值
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        //[Route("/")]
        [HttpGet]
        public IActionResult SignInWithGoogle()
        {
            ViewBag.ClientId = _configuration["Authentication:Google:ClientId"] ?? string.Empty;
            ViewBag.LoginUrl = Url.Action("Test", "Account") ?? string.Empty;
            return View();
        }

        //Google Auth成功之后会post request到data-login_uri指向的endpoint
        [HttpPost]
        public IActionResult Test(string credential)
        {
            Console.WriteLine(credential);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Route("/test")]
        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
