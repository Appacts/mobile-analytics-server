using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Client.DomainModel;
using AppActs.Client.DomainModel.Factory;
using AppActs.Client.Repository.Interface;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    [TestClass]
    public class TileRepositoryIntegration : TestBase
    {
        int applicationId = 7;
        DateTime dateStart = DateTime.Now.AddDays(-7);
        DateTime dateEnd = DateTime.Now;
        DateTime dateStartCompare = DateTime.Now.AddDays(-14);
        DateTime dateEndCompare = DateTime.Now.AddDays(-7);

        [TestMethod]
        [TestCategory("Integration")]
        public void TilesDontThrowDataAccessLayerException()
        {
            //ITileFactory iTileFactory = new TileTypeFactory();
            //ITileRepository iTiledal = new TileRepository(this.connectionString);

            //foreach (KeyValuePair<Guid, string> key in Dictionaries.TileToStoreProcedure)
            //{
            //    ITile iTile = iTileFactory.Create(key.Key);

            //    if (iTile is ITileWithTrend)
            //    {
            //        iTile = iTiledal.Load(iTile, key.Key, applicationId, dateStart, dateEnd, dateStartCompare, dateEndCompare);
            //    }
            //    else
            //    {
            //        iTile = iTiledal.Load(iTile, key.Key, applicationId, dateStart, dateEnd);
            //    }
            //}
            throw new NotImplementedException();
        }
    }
}
