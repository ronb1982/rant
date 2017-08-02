using RantApp.BLL.Interfaces;
using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;

namespace RantApp.BLL.ViewModels
{
    public class EmotionListModel
    {
        public int SelectedItemId { get; set; }
        public IList<SelectListItem> EmotionItems { get; set; }
        private static IReadRepository<Emotion> _readRepository;

        // Empty constructor
        public EmotionListModel() {}

        public EmotionListModel(IReadRepository<Emotion> readRepository)
        {
            _readRepository = readRepository;
            EmotionItems = new List<SelectListItem>();
            GetSelectList();
        }

        private void GetSelectList()
        {
            try
            {
                foreach (var emotion in GetEmotions())
                {
                    if (emotion != null)
                    {
                        EmotionItems.Add(
                            new SelectListItem() { Value = emotion.EmotionId.ToString(), Text = emotion.EmotionType });
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        public List<Emotion> GetEmotions()
        {
            List<Emotion> emotions = _readRepository.GetAll().ToList();
            return emotions;
        }
    }
}
