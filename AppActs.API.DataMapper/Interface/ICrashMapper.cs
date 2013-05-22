using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Crash;

namespace AppActs.API.DataMapper.Interface
{
    public interface ICrashMapper : ISave<Crash, CrashSummary>
    {

    }
}
