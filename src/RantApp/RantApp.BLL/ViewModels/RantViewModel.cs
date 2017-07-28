using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RantApp.BLL.Models;
using RantApp.BLL.Interfaces;
using System.Data.Entity;

namespace RantApp.BLL.ViewModels
{
    public class RantViewModel
    {
        public ICollection<Rant> Rants { get; set; }
        public ICollection<Reaction> Reactions { get; set; }

        private IRepository<Rant> _repository;

        public RantViewModel(IRepository<Rant> repository)
        {
            _repository = repository;
        }

        public List<Rant> GetRants()
        {
            List<Rant> rants = _repository.GetAll().Include(r => r.Reactions).ToList();
            return rants;
        }

        public Rant GetRantById(int rantId)
        {
            return _repository.GetById(rantId);
        }
    }
}
