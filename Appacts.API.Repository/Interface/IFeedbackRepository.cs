using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Feedback;

namespace AppActs.API.Repository.Interface
{
    public interface IFeedbackRepository
    {
        void Save(Feedback entity);

        void Save(FeedbackSummary entity);
    }
}
