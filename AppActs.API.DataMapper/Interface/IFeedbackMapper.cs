using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Feedback;

namespace AppActs.API.DataMapper.Interface
{
    public interface IFeedbackMapper : ISave<Feedback, FeedbackSummary>
    {

    }
}
