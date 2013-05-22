using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AppActs.Client.Data.Model.Enum;
using System.Web.UI.DataVisualization.Charting;

namespace AppActs.Client.WebSite.Base
{
    public static class Dictionaries
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<ChartType, SeriesChartType> chartTypeToSeriesChartType =
            new Dictionary<ChartType, SeriesChartType>()
            {
                { ChartType.Bar, SeriesChartType.Bar },
                { ChartType.Area, SeriesChartType.Area },
                { ChartType.Line, SeriesChartType.Line },
                { ChartType.Pie, SeriesChartType.Pie }
            };
    }
}