using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class GraphWithTabularCompare
    {
        public Graph Data { get; set; }
        public List<GraphSeries> Tabular { get; set; }
        public bool NotEnoughData { get; set; }

        public void Consume(GraphWithTabularCompare graphWithTabularCompare)
        {
            this.Data = graphWithTabularCompare.Data;
            this.Tabular = graphWithTabularCompare.Tabular;
            this.NotEnoughData = graphWithTabularCompare.NotEnoughData;
        }
    }
}
