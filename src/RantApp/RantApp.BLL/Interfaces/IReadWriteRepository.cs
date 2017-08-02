using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Interfaces
{
    public interface IReadWriteRepository<T> : IReadRepository<T>, IDisposable where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetById(int entityId);
    }
}
