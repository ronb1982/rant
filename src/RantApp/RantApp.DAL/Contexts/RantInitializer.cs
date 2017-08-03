using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.DAL.Contexts
{
    public class RantInitializer : DropCreateDatabaseIfModelChanges<RantContext>
    {
        protected override void Seed(RantContext context)
        {
            var emotions = new List<Emotion>()
            {
                new Emotion() { EmotionId = 1, EmotionType = "pissed off about" },
                new Emotion() { EmotionId = 2, EmotionType = "confused about" },
                new Emotion() { EmotionId = 3, EmotionType = "happy about" },
                new Emotion() { EmotionId = 4, EmotionType = "frustrated with" },
                new Emotion() { EmotionId = 5, EmotionType = "sad about" },
                new Emotion() { EmotionId = 6, EmotionType = "tired of" }
            };

            emotions.ForEach(e => context.Emotions.Add(e));
            context.SaveChanges();

            var rants = new List<Rant>()
            {
                new Rant()
                {
                    EmotionId = 1,
                    Title = string.Format("I'm {0} Donald Trump.", emotions.FirstOrDefault(e => e.EmotionId == 1).EmotionType),
                    PostDate = new DateTime(2016, 8, 18, 15, 25, 36),
                    PictureUrl = "",
                    Description = "Trump is such an idiot! Does anyone agree?"
                },
                new Rant()
                {
                    EmotionId = 2,
                    Title = string.Format("I'm {0} my current relationship.", emotions.FirstOrDefault(e => e.EmotionId == 2).EmotionType),
                    PostDate = new DateTime(2017, 7, 27, 20, 55, 44),
                    PictureUrl = "",
                    Description = "Why isn't my relationship working out? I lived with my girlfriend for some many years!",
                },
                new Rant()
                {
                    EmotionId = 3,
                    Title = string.Format("I'm {0} winning the lotto.", emotions.FirstOrDefault(e => e.EmotionId == 3).EmotionType),
                    PostDate = new DateTime(2017, 2, 3, 12, 02, 0),
                    PictureUrl = "",
                    Description = "How about winning the lotto on your first try? Not bad, eh?"
                }
            };

            rants.ForEach(r => context.Rants.Add(r));

            string reactionPrefix = "RE: ";

            var reactions = new List<Reaction>()
            {
                new Reaction()
                {
                    RantId = 1, PostDate = new DateTime(2016, 8, 18, 15, 28, 02),
                    Title = reactionPrefix + rants[0].Title,
                    Response = "Because he just is an idiot. Point blank!"
                },
                new Reaction()
                {
                    RantId = 1, PostDate = new DateTime(2016, 8, 19, 00, 28, 24),
                    Title = reactionPrefix + rants[0].Title,
                    Response = "I think he's doing a WONDERFUL job! Keep it going Mr. President!"
                },
                new Reaction()
                {
                    RantId = 1, PostDate = new DateTime(2016, 8, 19, 08, 10, 59),
                    Title = reactionPrefix + rants[0].Title,
                    Response = "OP is a troller. Remove her from this board!!!"
                }
            };

            reactions.ForEach(re => context.Reactions.Add(re));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
