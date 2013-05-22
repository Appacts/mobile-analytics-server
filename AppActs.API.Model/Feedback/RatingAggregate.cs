using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Feedback
{
    public class RatingAggregate : Aggregate<string>
    {
        public long Rating { get; set; }

        public RatingAggregate(string screenName, long rating)
            : base(screenName)
        {
            this.Key = screenName;
            this.Rating = rating;
        }

        public RatingAggregate CopyOnlyKey()
        {
            RatingAggregate ratingAgg = new RatingAggregate(this.Key, 0);
            return ratingAgg;
        }
    }
}
