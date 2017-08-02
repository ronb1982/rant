using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Interfaces
{
    public interface IReadRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll();
    }
}
