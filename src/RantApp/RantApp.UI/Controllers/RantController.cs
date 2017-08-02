using System.Web.Mvc;
using RantApp.BLL.ViewModels;
using RantApp.BLL.Interfaces;
using RantApp.DAL.Repositories;
using RantApp.BLL.Models;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;

namespace RantApp.UI.Controllers
{
    public class RantController : Controller
    {
        private static IReadWriteRepository<Rant> _rantRepository = new RantRepository();
        private static IReadRepository<Emotion> _emotionRepository = new EmotionRepository();

        public ViewResult Create()
        {
            RantViewModel viewModel = new RantViewModel(_rantRepository, _emotionRepository);
            viewModel.PartialViewName = "WizardTitleScreen";
            return View(viewModel);
        }

        public ActionResult WizardTitleScreen()
        {
            RantViewModel viewModel = new RantViewModel(_rantRepository, _emotionRepository);

            if (viewModel != null)
            {
                if (viewModel.CurrentRant == null)
                    viewModel.CurrentRant = new Rant();

                TempData["emotionItems"] = viewModel.Emotions.EmotionItems;

                return PartialView("WizardTitleScreen", viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        // Wizard Action Methods
        [HttpPost]
        public ActionResult WizardTitleScreen(RantViewModel viewModel, string prevBtn, string nextBtn)
        {
            if (nextBtn != null && ModelState.IsValid)
            {
                Rant modelRant = viewModel.CurrentRant;

                if (modelRant == null)
                {
                    return PartialView(viewModel);
                }

                Rant newRant = GetRantSession();
                newRant.EmotionId = viewModel.Emotions.SelectedItemId;
                viewModel.Emotions.EmotionItems = (List<SelectListItem>) TempData["emotionItems"];

                string emotionType = viewModel.Emotions.EmotionItems.FirstOrDefault(
                    e => e.Value == newRant.EmotionId.ToString()).Text;

                newRant.Title = string.Format("I'm {0} {1}", emotionType, modelRant.Title);
                viewModel.PartialViewName = "WizardTellUsMoreScreen";

                return View("Create", viewModel);
            }

            return PartialView();
        }

        public ActionResult WizardTellUsMoreScreen(RantViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.CurrentRant = GetRantSession();
                return PartialView("WizardTellUsMoreScreen", viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult WizardTellUsMoreScreen(RantViewModel viewModel,
            string prevBtn, string nextBtn)
        {
            Rant newRant = GetRantSession();

            if (prevBtn != null)
            {
                if (viewModel.CurrentRant == null)
                    viewModel.CurrentRant = new Rant();

                TempData["emotionItems"] = viewModel.Emotions.EmotionItems;
                viewModel.PartialViewName = "WizardTitleScreen";

                return View("Create", viewModel);
            }

            if (nextBtn != null && ModelState.IsValid)
            {
                Rant modelRant = viewModel.CurrentRant;

                if (modelRant == null)
                {
                    return PartialView(viewModel);
                }

                newRant.Description = viewModel.CurrentRant.Description;
                viewModel.PartialViewName = "WizardReactionTypeExpectedScreen";

                return View("Create", viewModel);
            }

            return PartialView(this);
        }

        [HttpPost]
        public PartialViewResult WizardReactionTypeExpectedScreen()
        {
            return PartialView();
        }

        public ActionResult Details(int? id)
        {
            RantViewModel viewModel = new RantViewModel(_rantRepository);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                Rant rant = viewModel.GetRantById((int) id);
                return View(rant);
            }

            return RedirectToAction("Index");
        }

        private Rant GetRantSession()
        {
            if (Session["rant"] == null)
            {
                Session["rant"] = new Rant();
            }
            return (Rant)Session["rant"];
        }
    }
}