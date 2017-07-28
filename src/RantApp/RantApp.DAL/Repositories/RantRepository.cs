using RantApp.BLL.Models;
using RantApp.BLL.Interfaces;
using RantApp.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.DAL.Repositories
{
    public class RantRepository : IRepository<Rant>
    {
        private static RantContext _context;

        public RantRepository()
        {
            _context = new RantContext();
        }

        public IQueryable<Rant> GetAll()
        {
            return _context.Rants;
        }

        public void Add(Rant entity)
        {
            try
            {
                _context.Rants.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        public void Delete(Rant entity)
        {
            try
            {
                _context.Rants.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        public Rant GetById(int entityId)
        {
            Rant rant = null;

            try
            {
                rant = _context.Rants.Find(entityId);
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }

            return rant;
        }

        public void Update(Rant entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
