using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.BLL.Models
{
    public class Emotion
    {
        [Required]
        public int EmotionId { get; set; }
        public string EmotionType { get; set; }

        // Navigation property
        public virtual Rant Rant { get; set; }
    }
}
