using Advanced.Net6.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Net6.Project.Controllers
{
    public class SixthController : Controller
    {

        public SixthController(IMircPhone mircPhone,IServiceProvider serviceProvider) 
        { 
        
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
