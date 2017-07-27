using RantApp.BLL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantApp.DAL.Contexts
{
    public class RantInitializer : DropCreateDatabaseAlways<RantContext>
    {
        protected override void Seed(RantContext context)
        {
            var rants = new List<Rant>
            {
                new Rant()
                {
                    Title = "I'm pissed off about Donald Trump.",
                    PostDate = new DateTime(2016, 8, 18, 15, 25, 36),
                    PictureUrl = "",
                    Description = "Trump is such an idiot! Does anyone agree?"
                },
                new Rant()
                {
                    Title = "I'm happy about winning the lotto.",
                    PostDate = new DateTime(2017, 2, 3, 12, 02, 0),
                    PictureUrl = "",
                    Description = "How about winning the lotto on your first try? Not bad, eh?"
                },
                new Rant()
                {
                    Title = "I'm confused about my current relationship.",
                    PostDate = new DateTime(2017, 7, 27, 20, 55, 44),
                    PictureUrl = "",
                    Description = "Why isn't my relationship working out? I lived with my girlfriend for some many years!"
                }
            };

            rants.ForEach(r => context.Rants.Add(r));
            context.SaveChanges();

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
