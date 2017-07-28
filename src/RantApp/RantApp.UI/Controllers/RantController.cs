using System.Web.Mvc;
using RantApp.BLL.ViewModels;
using RantApp.BLL.Interfaces;
using RantApp.DAL.Repositories;
using RantApp.BLL.Models;
using System.Net;

namespace RantApp.UI.Controllers
{
    public class RantController : Controller
    {
        private static IRepository<Rant> _repository = new RantRepository();

        // GET: Rant
        public ActionResult Index()
        {
            RantViewModel viewModel = new RantViewModel(_repository);
            viewModel.Rants = viewModel.GetRants();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetRant(int? id)
        {
            RantViewModel viewModel = new RantViewModel(_repository);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                Rant rant = viewModel.GetRantById((int) id);
                viewModel.Rants.Add(rant);
            }

            return View(viewModel);
        }
    }
}