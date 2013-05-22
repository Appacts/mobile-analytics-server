using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppActs.API.Model.Crash;

namespace AppActs.API.Repository.Interface
{
    public interface ICrashRepository
    {
        void Save(Crash crash);
        void Save(CrashSummary crash);
    }
}
