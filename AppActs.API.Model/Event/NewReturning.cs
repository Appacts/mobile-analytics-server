using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppActs.API.Model.Event
{
    public class NewReturningGroup
    {
        public int New { get; set; }
        public int Returning { get; set; }

        public NewReturningGroup()
        {

        }

        public NewReturningGroup(int newCount, int returningCount)
        {
            this.New = newCount;
            this.Returning = returningCount;
        }
    }
}
