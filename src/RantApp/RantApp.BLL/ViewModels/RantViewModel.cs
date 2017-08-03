using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RantApp.BLL.Models;
using RantApp.BLL.Interfaces;
using System.Data.Entity;
using System.Web.Mvc;
using System.Diagnostics;

namespace RantApp.BLL.ViewModels
{
    public class RantViewModel
    {
        // Read and Read/Write repository references
        private static IReadWriteRepository<Rant> _readWriteRepository;
        private static IReadRepository<Emotion> _readRepository;

        // Single Rant reference
        public Rant CurrentRant { get; set; }

        // Collections
        public ICollection<Rant> Rants { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public EmotionListModel Emotions { get; set; }

        // Empty constructor
        public RantViewModel() {}

        public RantViewModel(IReadWriteRepository<Rant> readWriteRepository)
        {
            Initialize(readWriteRepository);
        }

        public RantViewModel(IReadWriteRepository<Rant> readWriteRepository, IReadRepository<Emotion> readRepository)
        {
            Initialize(readWriteRepository, readRepository);
        }

        private void Initialize(IReadWriteRepository<Rant> readWriteRepository,
            IReadRepository<Emotion> readRepository = null)
        {
            if (readWriteRepository != null) _readWriteRepository = readWriteRepository;
            if (readRepository != null) _readRepository = readRepository;
            if (Rants == null) Rants = new List<Rant>();
            if (Reactions == null) Reactions = new List<Reaction>();
            if (Emotions == null && _readRepository != null) Emotions = new EmotionListModel(_readRepository);
        }

        public List<Rant> GetRants()
        {
            List<Rant> rants = _readWriteRepository.GetAll()
                .Include(r => r.Reactions)
                .ToList();

            return rants;
        }

        public Rant GetRantById(int rantId)
        {
            return _readWriteRepository.GetById(rantId);
        }

        public void SaveRant(Rant rant)
        {
            _readWriteRepository.Add(rant);
        }
    }
}
