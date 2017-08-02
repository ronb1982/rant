using RantApp.BLL.Interfaces;
using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.DAL.Repositories
{
    public class EmotionRepository : RepositoryBase, IReadRepository<Emotion>
    {
        public IQueryable<Emotion> GetAll()
        {
            return _context.Emotions;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
