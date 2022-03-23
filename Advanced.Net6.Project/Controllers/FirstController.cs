using Microsoft.AspNetCore.Mvc;

namespace Advanced.Net6.Project.Controllers
{
    public class FirstController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.User1 = "张三";
            ViewData["User2"] = "李四";
            TempData["User3"] = "王五";
            HttpContext.Session.SetString("User4", "赵六");
            object User5 = "田七";
            return View(User5);
        }
    }
}
