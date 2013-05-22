using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Device;

namespace AppActs.API.Model.Crash
{
    public class CrashSummary : Summary
    {
        public CrashSummary()
            : base()
        {

        }

        public CrashSummary(Crash crash)
            : base(crash)
        {

        }
    }
}
