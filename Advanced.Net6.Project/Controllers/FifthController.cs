using Advanced.Net6.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Net6.Project.Controllers
{
    public class FifthController : Controller
    {

        private readonly IMircPhone   mircPhone;

        private readonly IMircPhone mircPhone2;
        public  FifthController(IMircPhone _mircPhone,IServiceProvider provider) 
        {
           this.mircPhone = _mircPhone;
            this.mircPhone2 = provider.GetService<IMircPhone>();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
