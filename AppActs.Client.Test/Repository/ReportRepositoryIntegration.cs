using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppActs.Client.Repository.Interface;
using AppActs.Client.DomainModel;
using System.Data;

namespace AppActs.Client.Repository.SqlServer.MSTest
{
    [TestClass]
    public class ReportRepositoryIntegration : TestBase
    {
        //static readonly int ApplicationId = 7;
        //static readonly int AccountId = 1;

        //static readonly DateTime startDate = DateTime.Now.AddMonths(-12);
        //static readonly DateTime endDate = DateTime.Now;


        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemandUsage()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("D256457F-F9F3-4295-BDC1-33F97B785942"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeApplicationCrash()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("A62425D4-F588-455B-954A-206BAC0B53A4"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemandDayOfTheWeekSegmentation()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("10CAE358-A557-4AC8-86A0-9CF42B356D3D"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemandDaysSegmentation()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("0D5E7BB1-4AA2-4DE9-B816-E33F17A5A49F"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemandHoursSegmentation()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("2E31FE56-C3C9-4B13-9224-E702D82504AD"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemandSecondsSegmentation()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("952183BE-CFE4-40A5-906A-0A7223E6D8EA"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemographicsUserAge()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("1915C347-3A93-4D71-9715-9FBEE2E6B8C9"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemographicsUserLocation()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("578B12A4-BBA6-4FFB-94FF-FC6E4ECCFF95"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeDemographicsUserSex()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("1915C347-3A93-4D71-9715-9FBEE2E6B8C9"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeManufacturer()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("F28D0066-068A-4EC4-B37B-DCC67ECCEAED"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeOS()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("5EDBC08A-544A-4500-9DF9-DED4D9DD6A71"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeScreenError()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("7DB85161-859E-45E4-826B-1A5E9503101B"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeScreenEvents()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("50E8B11D-235C-4C77-8F7B-76E9C14F9B7E"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeScreenFeedback()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("A43B7B1C-ADB1-4401-935F-582F18CF74DD"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeScreenLoading()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("F80AA8C7-1EF6-4873-85B5-67907D02914C"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetGraphAxisReportNormalTypeScreenSession()
        //{
        //    List<GraphAxis> graphAxis = this.getGraphAxis(new Guid("D1F8F242-03E6-44E7-98D7-A142FD0063B9"));

        //    Assert.IsNotNull(graphAxis);
        //    Assert.IsTrue(graphAxis.Count > 0);
        //}

        //[TestMethod]
        //[TestCategory("Integration")]
        //public void GetDetailData()
        //{
        //    IDataRepository iReportDal = new ReportRepository(this.connectionString);

        //    System.Data.DataTable table = iReportDal.GetDetail(new Guid("111F3DD9-8EF4-450E-A382-F7C6F7A3F11C"), ApplicationId, startDate, endDate, "Blackberry");

        //    Assert.IsNotNull(table);
        //    Assert.IsTrue(table.Rows.Count > 0);
        //}

        ///// <summary>
        ///// Gets the graph axis.
        ///// </summary>
        ///// <param name="reportNormalType">Type of the report normal.</param>
        ///// <returns></returns>
        //private List<GraphAxis> getGraphAxis(string storedFunction)
        //{
        //    IDataRepository iReportDAL = new DataRepository(this.connectionString);

        //    List<GraphAxis> graphAxis =
        //        iReportDAL.GetGraphAxis
        //            (
        //                storedFunction,
        //                ApplicationId,
        //                startDate,
        //                endDate
        //            );

        //    return graphAxis;
        //}

    }
}
