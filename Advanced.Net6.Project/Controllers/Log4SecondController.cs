using Microsoft.AspNetCore.Mvc;

namespace Advanced.Net6.Project.Controllers
{
    public class Log4SecondController : Controller
    {
        public readonly ILogger<Log4SecondController> _Logger;

        //public readonly ILoggerFactory _LoggerFactory;

        public Log4SecondController(ILogger<Log4SecondController> logger) 
        {
            this._Logger = logger;
            this._Logger.LogInformation($"{this.GetType().Name}被构造了");
        }

        public IActionResult Index()
        {
            this._Logger.LogInformation($"Index被构造了");
            return View();
        }

        public IActionResult Level()
        {
            _Logger.LogDebug("this is  LogDebug");
            _Logger.LogInformation("this is  LogInformation");
            _Logger.LogWarning("this is  LogWarning");
            _Logger.LogError("this is  LogError");
            _Logger.LogTrace("this is  LogTrace");
            _Logger.LogCritical("this is  LogCritical");
            return new JsonResult( new { Success = true });
        }
    }
}
