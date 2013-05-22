using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class DataWithInfo
    {
        public Graph Data { get; set; }
        public GraphSeries Tabular { get; set; }
        public GraphInfo Info { get; set; }
    }
}
