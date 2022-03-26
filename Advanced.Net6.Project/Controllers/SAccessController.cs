using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advanced.Net6.Project.Controllers
{
    public class SAccessController : Controller
    {
        // GET: SAccessController1
        public ActionResult Index()
        {
            return View();
        }

        // GET: SAccessController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SAccessController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SAccessController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SAccessController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SAccessController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SAccessController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SAccessController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
