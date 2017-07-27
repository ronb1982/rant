using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RantApp.BLL.Models;

namespace RantApp.DAL.Contexts
{
    public class RantContext : DbContext
    {
        public RantContext() : base("name=RantContext") { }

        public DbSet<Rant> Rants { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
