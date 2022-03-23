using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Advanced.Net6.Project.Controllers
{
    public class FourthController : Controller
    {
        [Authorize(AuthenticationSchemes= CookieAuthenticationDefaults.AuthenticationScheme ,Roles ="Admin")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "User")]
        public IActionResult Index()
        {

            var role = HttpContext.User;
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Thear,Admin")]
        public IActionResult Index1()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy  = "rolePolicy")]
        public IActionResult Index3()
        {  //策略授权
            return View();
        }



        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "rolePolicy")]
        public IActionResult Index4()
        {  
            return View();
        }
        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            if ("Richard".Equals(name) && "1".Equals(password))
            {
                var claims = new List<Claim>()//鉴别你是谁，相关信息
                    {
                        new Claim("Userid","1"),
                        new Claim(ClaimTypes.Role,"Admin"),
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim(ClaimTypes.Name,$"{name}--来自于Cookies"),
                        new Claim(ClaimTypes.Email,$"18672713698@163.com"),
                        new Claim("password",password),//可以写入任意数据
                        new Claim("Account","Administrator"),
                        new Claim("role","admin"),
                         new Claim("QQ","1030499676")
                    };
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Customer"));
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(1),//过期时间：30分钟

                }).Wait();
                var user = HttpContext.User;
                return base.Redirect("/Fourth/Index");
            }
            else
            {
                base.ViewBag.Msg = "用户或密码错误";
            }
            return await Task.FromResult<IActionResult>(View());
        }
    }
}
