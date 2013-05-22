using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Feedback
{
    public class FeedbackSummary : Summary
    {
        public List<RatingAggregate> Ratings { get; set; }
        public int SumOfRatings { get; set; }

        public FeedbackSummary()
            : base()
        {

        }

        public FeedbackSummary(Feedback feedback)
            : base(feedback)
        {
            this.Ratings = new List<RatingAggregate>();
            this.Ratings.Add(new RatingAggregate(feedback.ScreenName, 
                (int)feedback.Rating));

            this.SumOfRatings += (int)feedback.Rating;
        }
    }
}
