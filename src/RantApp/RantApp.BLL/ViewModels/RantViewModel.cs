using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.ViewModels
{
    public class RantViewModel
    {
        public IEnumerable<Rant> Rant { get; set; }
        public IEnumerable<Reaction> Reactions { get; set; }
    }
}
