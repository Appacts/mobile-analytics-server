using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.API.Repository.Interface;
using AppActs.DomainModel;
using AppActs.Core.Exceptions;

namespace AppActs.API.Repository.MSTest
{
    [TestClass]
    public class FeedbackRepositoryIntegration : TestBase
    {
        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(DataAccessLayerException))]
        public void SaveThrowsDataAccessLayerException()
        {
            IFeedbackRepository iFeedbackRepository = new FeedbackRepository(this.connectionString);

            iFeedbackRepository.Save(0, 0, new FeedbackItem("feedback", AppActs.DomainModel.Enum.RatingType.Five, "Main", Guid.NewGuid(), DateTime.Now, "1.1.1.1"));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Save()
        {
            IFeedbackRepository iFeedbackRepository = new FeedbackRepository(this.connectionString);

            FeedbackItem feedBackItem = new FeedbackItem
                (
                    "feedback",
                     AppActs.DomainModel.Enum.RatingType.Five,
                     "screenName",
                     Guid.NewGuid(),
                     DateTime.Now,
                     "1.1"
                );

            iFeedbackRepository.Save(this.Application.Id, this.Device.Id, feedBackItem);
        }
    }
}
