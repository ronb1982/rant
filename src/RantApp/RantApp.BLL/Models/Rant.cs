using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Models
{
    public class Rant
    {
        public enum Emotion
        {
            Angry,
            Frustrated,
            Happy,
            Sad,
            Confused
        }

        public int RantId { get; set; }
        public DateTime PostDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        // Navigation property
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
