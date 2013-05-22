using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.Client.Model
{
    public class GraphWithTabularCompare<TOption, TSelect>
        : GraphWithTabularCompare
    {
        public List<TOption> Options { get; set; }
        public List<TSelect> Selected { get; set; }

        public GraphWithTabularCompare()
        {
            this.Options = new List<TOption>();
            this.Selected = new List<TSelect>();
        }
    }
}
