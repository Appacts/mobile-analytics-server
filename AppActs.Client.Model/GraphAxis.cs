using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class GraphAxis
    {
        public object X { get; set; }
        public object Y { get; set; }
        public object YY { get; set; }

        public GraphAxis()
        {

        }

        public GraphAxis(string x)
        {
            this.X = x;
        }

        public GraphAxis(string x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public GraphAxis(string x, float y, float yy)
        {
            this.X = x;
            this.Y = y;
            this.YY = yy;
        }
    }
}
