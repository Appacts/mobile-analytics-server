using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class GraphSeries
    {
        public string Name { get; set; }
        public List<GraphAxis> Axis { get; set; }

        public GraphSeries()
        {

        }

        public GraphSeries(string name, List<GraphAxis> graphAxis)
        {
            this.Name = name;
            this.Axis = graphAxis;
        }
    }
}
