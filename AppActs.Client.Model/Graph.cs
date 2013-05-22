using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.Client.Model.Enum;

namespace AppActs.Client.Model
{
    public class Graph
    {
        public string XLabel { get; private set; }
        public Type XType { get; private set; }
        public string YLabel { get; private set; }
        public Type YType { get; private set; }
        public string YYLabel { get; private set; }
        public Type YYType { get; private set; }
        public List<GraphSeries> Series { get; set; }
        public ChartType ChartType { get; set; }
        public bool IsDetailAvailable { get; set; }
        public bool IsDescriptionAvailable { get; set; }

        public Graph()
        {

        }

        public Graph(ReportDefinition reportDefinition, ReportDefinitionReport report)
        {
            this.XLabel = reportDefinition.X;
            this.XType = reportDefinition.XType;
            this.YLabel = reportDefinition.Y;
            this.YType = reportDefinition.YYType;
            this.YYLabel = reportDefinition.YY;
            this.YYType = reportDefinition.YYType;
            this.ChartType = report.ChartType;
            this.Series = new List<GraphSeries>();
        }

        public bool ContainsData()
        {
            return this.Series.Count > 0 && this.Series[0].Axis.Count > 0;
        }
    }
}
