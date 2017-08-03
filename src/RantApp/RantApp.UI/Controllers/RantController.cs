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
            return View(viewModel);
        }

        public PartialViewResult _WizardTitleScreen(RantViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new RantViewModel(_rantRepository, _emotionRepository);

            if (viewModel != null)
            {
                if (viewModel.CurrentRant == null)
                    viewModel.CurrentRant = new Rant();

                TempData["emotionItems"] = viewModel.Emotions.EmotionItems;

                return PartialView("_WizardTitleScreen", viewModel);
            }

            return PartialView(this);
        }

        // Wizard Action Methods
        [HttpPost]
        public PartialViewResult _WizardTitleScreen(RantViewModel viewModel, string prevBtn, string nextBtn)
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
                viewModel.Emotions.EmotionItems = (List<SelectListItem>)TempData["emotionItems"];

                string emotionType = viewModel.Emotions.EmotionItems.FirstOrDefault(
                    e => e.Value == newRant.EmotionId.ToString()).Text;

                newRant.Title = string.Format("I'm {0} {1}", emotionType, modelRant.Title);

                return PartialView("_WizardTellUsMoreScreen", viewModel);
            }

            return PartialView();
        }

        [HttpPost]
        public ActionResult _WizardTellUsMoreScreen(RantViewModel viewModel,
            string prevBtn, string nextBtn)
        {
            Rant newRant = GetRantSession();

            if (prevBtn != null)
            {
                if (viewModel.CurrentRant == null)
                    viewModel.CurrentRant = new Rant();

                TempData["emotionItems"] = viewModel.Emotions.EmotionItems;

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

                return PartialView("_WizardReactionTypeExpectedScreen", viewModel);
            }

            return PartialView(this);
        }

        [HttpPost]
        public ActionResult _WizardReactionTypeExpectedScreen(RantViewModel viewModel,
            string prevBtn, string nextBtn)
        {
            Rant newRant = GetRantSession();

            if (prevBtn != null)
            {
                if (viewModel.CurrentRant == null)
                    viewModel.CurrentRant = new Rant();

                return View("Create", viewModel);
            }

            if (nextBtn != null && ModelState.IsValid)
            {
                Rant modelRant = viewModel.CurrentRant;

                if (modelRant == null)
                {
                    return PartialView(viewModel);
                }

                newRant.ExpectedReactionRequest = viewModel.CurrentRant.ExpectedReactionRequest;

                return RedirectToAction("_WizardSummary", viewModel);
            }

            return PartialView(this);
        }

        public PartialViewResult _WizardSummary(RantViewModel viewModel)
        {
            if (viewModel == null)
                viewModel = new RantViewModel(_rantRepository, _emotionRepository);

            if (viewModel != null)
            {
                viewModel.CurrentRant = GetRantSession();
                return PartialView("_WizardSummary", viewModel);
            }

            return PartialView(this);
        }

        [HttpPost]
        public ActionResult _WizardSummary(RantViewModel viewModel, string prevBtn, string doneBtn)
        {
            if (prevBtn != null)
            {
                // TODO: Redirect
                return null;
            }

            if (doneBtn != null && ModelState.IsValid)
            {
                viewModel.CurrentRant = GetRantSession();
                viewModel.CurrentRant.PostDate = DateTime.Now.ToLocalTime();
                viewModel.SaveRant(viewModel.CurrentRant);
                return RedirectToAction("Index", "Home");
            }

            // TOOD
            return null;
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
                Rant rant = viewModel.GetRantById((int)id);
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