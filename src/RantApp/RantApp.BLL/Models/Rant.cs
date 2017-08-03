using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Models
{
    public class Rant
    {
        public int RantId { get; set; }

        public int EmotionId { get; set; }
        public DateTime PostDate { get; set; }

        [Required(ErrorMessage = "Oops, you forgot to give us a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Hey, don't forget to tell us about how you feel!")]
        public string Description { get; set; }

        public string ExpectedReactionRequest { get; set; }

        public string PictureUrl { get; set; }

        // Navigation property
        public virtual ICollection<Reaction> Reactions { get; set; }
    }
}
