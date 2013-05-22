using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class LoyalyGroup
    {
        public int Once { get; set; }
        public int Twice { get; set; }
        public int _3_5 { get; set; }
        public int _6_9 { get; set; }
        public int _10_19 { get; set; }
        public int _20_49 { get; set; }
        public int _50_99 { get; set; }
        public int _100_199 { get; set; }
        public int _200_499 { get; set; }
        public int Over500 { get; set; }
    }
}
