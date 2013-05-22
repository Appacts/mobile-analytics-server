using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Specialized;

namespace AppActs.API.Model.Feedback
{
    public class Feedback : ItemWithScreen
    {
        [BsonElement("msg")]
        public string Message { get; set; }

        [BsonElement("rat")]
        public FeedbackRatingType Rating { get; set; }

        public Feedback()
            : base()
        {

        }

        public Feedback(NameValueCollection keyValues)
            : base(keyValues)
        {
            this.Message = keyValues[Keys.FEEDBACK_MESSAGE];
            this.Rating = (FeedbackRatingType)int.Parse(keyValues[Keys.FEEDBACK_RATING_TYPE]);
        }
    }
}
