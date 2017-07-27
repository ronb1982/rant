using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Models
{
    public class Reaction
    {
        public int ReactionId { get; set; }

        // Foreign Key
        public int RantId { get; set; }

        public DateTime PostDate { get; set; }
        public string Title { get; set; }
        public string Response { get; set; }

        // Navigation Properties
        public virtual Rant Rant { get; set; }
        public virtual ICollection<Reaction> OtherReactions { get; set; }
    }
}
