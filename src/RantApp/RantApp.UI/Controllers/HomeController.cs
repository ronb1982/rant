using RantApp.BLL.Interfaces;
using RantApp.BLL.Models;
using RantApp.BLL.ViewModels;
using RantApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RantApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private static IReadWriteRepository<Rant> _repository = new RantRepository();

        // GET: Rant
        public ActionResult Index()
        {
            RantViewModel viewModel = new RantViewModel(_repository);
            viewModel.Rants = viewModel.GetRants();

            return View(viewModel);
        }
    }
}