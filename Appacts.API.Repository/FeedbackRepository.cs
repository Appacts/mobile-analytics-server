using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Repository.Interface;
using AppActs.API.Model;
using AppActs.Core.Exceptions;
using System.Data.SqlClient;
using MongoDB.Driver;
using AppActs.API.DataMapper.Interface;
using AppActs.API.Model.Feedback;

namespace AppActs.API.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        readonly IFeedbackMapper feedbackMapper;

        public FeedbackRepository(IFeedbackMapper feedbackMapper)
        {
            this.feedbackMapper = feedbackMapper;
        }

        public void Save(Feedback entity)
        {
            this.feedbackMapper.Save(entity);
        }

        public void Save(FeedbackSummary entity)
        {
            this.feedbackMapper.Save(entity);
        }
    }
}
